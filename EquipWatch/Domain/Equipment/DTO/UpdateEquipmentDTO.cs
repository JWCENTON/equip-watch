﻿namespace Domain.Equipment.DTO;

public class UpdateEquipmentDTO
{
    public string Category { get; set; }
    public string Location { get; set; }
    public int Condition { get; set; }
    public Company.Models.Company Company { get; set; }
    public bool IsCheckedOut { get; set; }
    public User.Models.User? CheckedOutBy { get; set; }

    
}