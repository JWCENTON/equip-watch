﻿namespace DTO.ClientDTOs;

public class FullClientDTO
{
    public Guid Id { get; set; }
    public Domain.Company.Models.Company Company { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ContactAddress { get; set; }
}