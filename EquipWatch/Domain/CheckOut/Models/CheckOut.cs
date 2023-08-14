﻿namespace Domain.CheckOut.Models;

public class CheckOut
{
    public Guid Id { get; set; }
    public Guid EquipmentId { get; set; }
    public string UserId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ArriveTime { get; set; }
    public DateTime EndTime { get; set; }
    public Equipment.Models.Equipment Equipment { get; set; }
}