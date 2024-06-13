using Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController(IServiceManager _serviceManager) : ApiController(_serviceManager)
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _serviceManager.UserService.GetUsers();
        return Ok(users);
    }
    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserWithMail(string email)
    {
        var user = await _serviceManager.UserService.GetUserWithEmail(email);
        return Ok(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{userId}/{ban}")]
    public async Task<IActionResult> BanUser(int userId ,Ban ban)
    {
        await _serviceManager.UserService.UserBanStatusChange(userId, ban);
        return Ok($"User {ban} Successfully");
    }


}
