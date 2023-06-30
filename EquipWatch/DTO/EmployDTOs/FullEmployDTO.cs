﻿using Domain.Employ;

namespace DTO.EmployDTOs;

public class FullEmployDTO
{
    public Domain.Company.Models.Company Company { get; set; }
    public Domain.User.Models.User User { get; set; }
    public Role Role { get; set; }
}