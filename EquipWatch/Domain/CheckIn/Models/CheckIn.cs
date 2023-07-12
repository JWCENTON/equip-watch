﻿namespace Domain.CheckIn.Models;

public class CheckIn
{
    public Guid Id { get; set; }
    public Equipment.Models.Equipment? Equipment { get; set; }
    public Employee.Models.Employee? Employee { get; set; }
    public DateTime Time { get; set; }
}