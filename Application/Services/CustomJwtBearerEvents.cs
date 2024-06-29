using Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace Application.Services;

public class CustomJwtBearerEvents : JwtBearerEvents
{
    private readonly IRepositoryManager _repositoryManager;

    public CustomJwtBearerEvents(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public override async Task TokenValidated(TokenValidatedContext context)
    {
        // Custom logic for handling token validation
        Console.WriteLine("Custom event: Token validated");

        var userIdClaim = context.Principal;
        if (userIdClaim != null)
        {
            var user = await _repositoryManager.UserRepository.GetUserFromClaim(userIdClaim);
            if (user != null && user.Banned == Ban.Banned)
            {
                context.Fail("User is banned");
            }
        }

        await base.TokenValidated(context);
    }
}
