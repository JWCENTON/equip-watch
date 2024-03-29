﻿namespace Domain.BookedEquipment.Models;

public class BookedEquipment
{
    public Guid Id { get; set; }
    public Guid CommissionId { get; set; }
    public Guid? ReservationId { get; set; }
    public Guid? EquipmentInUseId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? EndTime { get; set; }
    public Commission.Models.Commission.Commission Commission { get; set; }
    public Reservation.Models.Reservation? Reservation { get; set;}
    public EquipmentInUse.Models.EquipmentInUse EquipmentInUse { get; set; }
}