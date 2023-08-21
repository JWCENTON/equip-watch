﻿using DTO.EmployDTOs;
using DTO.EquipmentDTOs;
using DTO.UserDTOs;

namespace DTO.CheckInDTOs;

public record UpdateCheckInDTO
{
    public string? UserId { get; init; }
    public string? EquipmentId { get; init; }
}