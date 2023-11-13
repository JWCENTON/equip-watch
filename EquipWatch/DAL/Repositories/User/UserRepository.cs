﻿using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly UserManager<Domain.User.Models.User> _userManager;
    private readonly SignInManager<Domain.User.Models.User> _signInManager;

    public UserRepository(UserManager<Domain.User.Models.User> userManager, SignInManager<Domain.User.Models.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Domain.User.Models.User> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null) return user;
        throw new InvalidOperationException(message:"User not found");
    }

    public async Task<bool> IsEmailConfirmedAsync(Domain.User.Models.User user)
    {
        var result = await _userManager.IsEmailConfirmedAsync(user);
        return result;
    }

    public async Task<SignInResult> PasswordSignInAsync(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
        return result;
    }
}