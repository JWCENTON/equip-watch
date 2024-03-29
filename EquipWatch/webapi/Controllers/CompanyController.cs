﻿using AutoMapper;
using Domain.Company.Models;
using DTO.CompanyDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.uow;
using webapi.Validators;

namespace webapi.Controllers;

[Authorize]
[ApiController, Route("api/[controller]")]
public class CompanyController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateCompanyDTOValidator _createCompanyValidator;
    private readonly UpdateCompanyDTOValidator _updateCompanyValidator;
    
    public CompanyController(IUnitOfWork unitOfWork, IMapper mapper,
        CreateCompanyDTOValidator createValidator,
        UpdateCompanyDTOValidator updateValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createCompanyValidator = createValidator;
        _updateCompanyValidator = updateValidator;
    }

    [HttpGet("{id}")]
    public async Task<Company> GetCompany(Guid id)
    {
        return await _unitOfWork.Companies.GetAsync(id);
    }

    [HttpGet]
    public async Task<Company> Get()
    {
        return await _unitOfWork.Companies.GetAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Company>> CreateCompany([FromBody] CreateCompanyDTO companyDto)
    {
        var result = await _createCompanyValidator.ValidateAsync(companyDto);
        if (result.IsValid)
        {
            var company = _mapper.Map<Company>(companyDto);
            company.Id = Guid.NewGuid();

            await _unitOfWork.Companies.CreateAsync(company);
            return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, _mapper.Map<FullCompanyDTO>(company));
        }
        return BadRequest(result.Errors.First().ErrorMessage);
    }
}