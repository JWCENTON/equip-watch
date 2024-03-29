﻿using DTO.ClientDTOs;
using FluentValidation;

namespace webapi.Validators;

public abstract class BaseClientDTOValidator<T> : AbstractValidator<T> where T : BaseClientDTO
{
    protected BaseClientDTOValidator()
    {
        RuleFor(dto => dto.FirstName).SetValidator(new ClientFirstNameValidator());

        RuleFor(dto => dto.LastName).SetValidator(new ClientLastNameValidator());

        RuleFor(dto => dto.Email).SetValidator(new ClientEmailValidator());

        RuleFor(dto => dto.PhoneNumber).SetValidator(new ClientPhoneNumberValidator());

        RuleFor(dto => dto.ContactAddress).SetValidator(new ClientContactAddressValidator());
    }
}

public class CreateClientDTOValidator : BaseClientDTOValidator<CreateClientDTO>
{
    public CreateClientDTOValidator()
    {
        RuleFor(dto => dto.CompanyId).SetValidator(new CompanyIdValidator());
    }
}

public class FullClientDTOValidator : BaseClientDTOValidator<FullClientDTO>
{
    public FullClientDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new ClientIdValidator());
        RuleFor(dto => dto.CompanyId).SetValidator(new CompanyIdValidator());
    }
}

public class PartialClientDTOValidator : BaseClientDTOValidator<PartialClientDTO>
{
    public PartialClientDTOValidator()
    {
        RuleFor(dto => dto.Id).SetValidator(new ClientIdValidator());
    }
}

public class UpdateClientDTOValidator : AbstractValidator<UpdateClientDTO>
{
    public UpdateClientDTOValidator()
    {
        RuleFor(dto => dto.CompanyId).SetValidator(new CompanyIdValidator()!)
            .When(dto => dto.CompanyId != null);

        RuleFor(dto => dto.FirstName).SetValidator(new ClientFirstNameValidator()!)
            .When(dto => dto.FirstName != null);

        RuleFor(dto => dto.LastName).SetValidator(new ClientLastNameValidator()!)
            .When(dto => dto.LastName != null);

        RuleFor(dto => dto.Email).SetValidator(new ClientEmailValidator()!)
            .When(dto => dto.Email != null);

        RuleFor(dto => dto.PhoneNumber).SetValidator(new ClientPhoneNumberValidator()!)
            .When(dto => dto.PhoneNumber != null);

        RuleFor(dto => dto.ContactAddress).SetValidator(new ClientContactAddressValidator()!)
            .When(dto => dto.ContactAddress != null);

    }
}

internal class ClientIdValidator : AbstractValidator<string>
{
    internal ClientIdValidator()
    {
        RuleFor(id => id)
            .NotEmpty()
            .WithMessage("Client ID cannot be empty.")
            .Must(GuidValidator.ValidateGuid)
            .WithMessage("Invalid Client ID.");
    }
}
internal class ClientFirstNameValidator : AbstractValidator<string>
{
    internal ClientFirstNameValidator()
    {
        RuleFor(firstName => firstName)
            .NotEmpty()
            .WithMessage("First name cannot be empty.")
            .MaximumLength(50)
            .WithMessage("First name cannot exceed 50 characters.");
    }
}
internal class ClientLastNameValidator : AbstractValidator<string>
{
    internal ClientLastNameValidator()
    {
        RuleFor(lastName => lastName)
            .NotEmpty()
            .WithMessage("Last name cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Last name cannot exceed 50 characters.");
    }
}

internal class ClientEmailValidator : AbstractValidator<string>
{
    internal ClientEmailValidator()
    {
        RuleFor(email => email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(50)
            .WithMessage("Email cannot exceed 50 characters.");
    }
}

internal class ClientPhoneNumberValidator : AbstractValidator<string>
{
    internal ClientPhoneNumberValidator()
    {
        RuleFor(phoneNumber => phoneNumber)
            .NotEmpty()
            .WithMessage("Phone number cannot be empty.")
            .Matches(@"^\d{9}$")
            .WithMessage("Invalid phone number format. Phone number must be 9 digits.");
    }
}

internal class ClientContactAddressValidator : AbstractValidator<string>
{
    internal ClientContactAddressValidator()
    {
        RuleFor(contactAddress => contactAddress)
            .NotEmpty()
            .WithMessage("Contact address cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Contact address cannot exceed 50 characters.");
    }
}