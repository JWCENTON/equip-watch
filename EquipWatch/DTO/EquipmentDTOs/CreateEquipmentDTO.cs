﻿using Domain.Company.Models;
using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EquipmentDTOs;

public record CreateEquipmentDTO
{
    public string SerialNumber { get; init; }
    public string Category { get; init; }
    public string Location { get; init; }
    public int Condition { get; init; }
    public CompanyIdDTO Company { get; init; }
}