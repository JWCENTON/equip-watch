﻿using DTO.CompanyDTOs;
using DTO.UserDTOs;

namespace DTO.EquipmentDTOs;

public record CreateEquipmentDTO
{
    public string SerialNumber { get; init; }
    public string Category { get; init; }
    public string Location { get; init; }
    public int Condition { get; init; }
    public FullCompanyDTO Company { get; init; }
    public bool IsCheckedOut { get; init; }
    public FullUserDTO? CheckedOutBy { get; init; }


}