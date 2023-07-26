using System.Diagnostics;
using System.Security.Claims;
 using System.Diagnostics;
using AutoMapper;
using Domain.CheckIn.Models;
using Domain.CheckOut.Models;
using Microsoft.AspNetCore.Mvc;
using Domain.Equipment.Models;
using DTO.EquipmentDTOs;
using Microsoft.AspNetCore.Authorization;
using Domain.User.Models;
using Microsoft.AspNetCore.Identity;
using webapi.uow;
using webapi.Validators;
using System.Linq;

namespace webapi.Controllers;

[Authorize]
[ApiController, Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateEquipmentDTOValidator _createValidator;
    private readonly UpdateEquipmentDTOValidator _updateValidator;
    private readonly LocationEquipmentDTOValidator _locationValidator;

    public EquipmentController(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper, CreateEquipmentDTOValidator createValidator, UpdateEquipmentDTOValidator updateValidator, LocationEquipmentDTOValidator locationValidator)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _locationValidator = locationValidator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<List<PartialEquipmentDTO>> GetAll()
    {
        var data = await _unitOfWork.Equipments.GetAllAsync();
        return data.Select(equipment => _mapper.Map<PartialEquipmentDTO>(equipment)).ToList();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<Equipment> Get(Guid id)
    {
        return await _unitOfWork.Equipments.GetAsync(id);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreateEquipmentDTO>> CreateEquipment([FromBody] CreateEquipmentDTO equipmentDto)
    {
        var result = await _createValidator.ValidateAsync(equipmentDto);
        if (result.IsValid){
            var equipment = _mapper.Map<Equipment>(equipmentDto);

            equipment.Company = await _unitOfWork.Companies.GetAsync(equipment.CompanyId);

            equipment.Id = Guid.NewGuid();
            await _unitOfWork.Equipments.CreateAsync(equipment);
            return CreatedAtAction(nameof(Get), new { id = equipment.Id }, _mapper.Map<FullEquipmentDTO>(equipment));
        }
        throw new ArgumentException(result.Errors.First().ErrorMessage);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateEquipment(Guid id, [FromBody] UpdateEquipmentDTO equipmentDto)
    {
        var result = await _updateValidator.ValidateAsync(equipmentDto);
        if (result.IsValid)
        {
            var equipment = await _unitOfWork.Equipments.GetAsync(id);
            _mapper.Map(equipmentDto, equipment);
            if (equipmentDto.CompanyId != null)
            {
                equipment.Company = await _unitOfWork.Companies.GetAsync(equipment.CompanyId);
            }
            await _unitOfWork.Equipments.UpdateAsync(equipment);
            return NoContent();
        }
        throw new ArgumentException(result.Errors.First().ErrorMessage);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _unitOfWork.Equipments.RemoveAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}/checkout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Checkout(Guid id, [FromBody] UpdateEquipmentLocationDTO locationDto)
    {
        var result = await _locationValidator.ValidateAsync(locationDto);
        if (result.IsValid)
        {
            var equipment = await _unitOfWork.Equipments.GetAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (equipment.IsCheckedOut || userId == null) { return BadRequest(); }
            equipment.IsCheckedOut = true;
            equipment.Location = locationDto.Location;
            await _unitOfWork.Equipments.UpdateAsync(equipment);

            var checkout = new CheckOut
            {
                Id = Guid.NewGuid(),
                Equipment = equipment,
                UserId = userId,
                Time = DateTime.Now
            };

            await _unitOfWork.CheckOuts.CreateAsync(checkout);

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
        throw new ArgumentException(result.Errors.First().ErrorMessage);
    }

    [HttpPatch("{id}/checkin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CheckIn(Guid id, [FromBody] UpdateEquipmentLocationDTO locationDto)
    {
        var result = await _locationValidator.ValidateAsync(locationDto);
        if (result.IsValid)
        {
            var equipment = await _unitOfWork.Equipments.GetAsync(id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!equipment.IsCheckedOut || userId == null) { return BadRequest(); }

            equipment.IsCheckedOut = false;
            equipment.Location = locationDto.Location;
            await _unitOfWork.Equipments.UpdateAsync(equipment);

            var checkIn = new CheckIn
            {
                Id = Guid.NewGuid(),
                Equipment = equipment,
                UserId = userId,
                Time = DateTime.Now
            };

            await _unitOfWork.CheckIns.CreateAsync(checkIn);

            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
        throw new ArgumentException(result.Errors.First().ErrorMessage);
    }

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(Guid id)
    //{
    //    try
    //    {
    //        await _unitOfWork.Equipments.RemoveAsync(id);
    //        return Ok();
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}
}
