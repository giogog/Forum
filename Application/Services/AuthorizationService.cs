using Contracts;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;

namespace Application.Services;

public class AuthorizationService : IAuthorizationService
{

    private readonly RoleManager<Role> _roleManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IRepositoryManager _repositoryManager;



    public AuthorizationService(RoleManager<Role> roleManager,
        ITokenGenerator tokenGenerator,
        IRepositoryManager repositoryManager)
    {

        _roleManager = roleManager;
        _tokenGenerator = tokenGenerator;
        _repositoryManager = repositoryManager;
    }

    public async Task<LoginResponseDto> Authenticate(Expression<Func<User, bool>> expression)
    {
        var user = await _repositoryManager.UserRepository.GetUser(expression);

        var token = await _tokenGenerator.GenerateToken(user);

        return new LoginResponseDto(user.Id,user.UserName, token);
    }

    public async Task<IdentityResult> Register(RegisterDto registerDto)
    {
        var user = new User
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            Name = registerDto.Name,
            Surname = registerDto.Surname
        };

        var result = await _repositoryManager.UserRepository.CreateUser(user, registerDto.Password);

        if (!result.Succeeded)
            return result;
        try
        {
            var roleExists = await _roleManager.RoleExistsAsync("User");

            if (roleExists)
                return await _repositoryManager.UserRepository.AddToRole(user, "User");
            else
                return IdentityResult.Failed(new IdentityError { Code = "Exception", Description = "Role don't Exists" });
        }
        catch (Exception ex)
        {
            return IdentityResult.Failed(new IdentityError { Code = "Exception", Description = ex.Message });
        }

    }

    public async Task<IdentityResult> Login(LoginDto loginDto)
    {

        var user = await _repositoryManager.UserRepository.GetUser(user => user.UserName == loginDto.Username);

        if (user == null)
            return IdentityResult.Failed(new IdentityError { Code = "UserDoesNotExist", Description = "The user does not exists." });

        var PasswordCheck = await _repositoryManager.UserRepository.CheckPassword(user, loginDto.Password);
        if (!PasswordCheck)
            return IdentityResult.Failed(new IdentityError { Code = "IncorrectPassword", Description = "Incorrect password." });
        if (!user.EmailConfirmed)
            return IdentityResult.Failed(new IdentityError { Code = "MailisNotConfirmed", Description = "Please Confirm Mail." });

        return IdentityResult.Success;

    }

    public async Task<IdentityResult> AddNewRole(string newRole)
    {
        var roleExists = await _roleManager.RoleExistsAsync("User");

        if (roleExists)
            return IdentityResult.Failed(new IdentityError { Code = "AlreadyExists",Description = "Role Already Exists" });
        Role role = new Role()
        {
            Name = newRole,
            NormalizedName = newRole.ToUpper(),
        };
        return await _roleManager.CreateAsync(role);
            
    }
}