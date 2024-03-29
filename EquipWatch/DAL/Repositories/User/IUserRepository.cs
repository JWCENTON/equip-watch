﻿using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories.User;

public interface IUserRepository
{
    Task<Domain.User.Models.User> FindByEmailAsync(string email);
    Task<bool> IsEmailConfirmedAsync(Domain.User.Models.User user);
    Task<SignInResult> PasswordSignInAsync(string email, string password);
    Task<IdentityResult> CreateAsync(Domain.User.Models.User user, string password);
    Task<string> GenerateEmailConfirmationTokenAsync(Domain.User.Models.User user);
    Task<Domain.User.Models.User?> FindByIdAsync(string userId);
    Task<IdentityResult> ConfirmEmailAsync(Domain.User.Models.User user, string token);
    Task<string> GeneratePasswordResetTokenAsync(Domain.User.Models.User user);
    Task<IdentityResult> ResetPasswordAsync(Domain.User.Models.User user, string token, string newPassword);
    Task<IdentityResult> UpdateAsync(Domain.User.Models.User user);
    Task<IdentityResult> ChangePasswordAsync(Domain.User.Models.User user, string currentPassword, string newPassword);
    Task<IQueryable<Domain.User.Models.User>> GetAll();
    Task<List<Domain.User.Models.User>> GetAvailable(List<string> assignedUserIds);
}