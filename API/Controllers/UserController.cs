using Contracts;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

public class UserController(IServiceManager _serviceManager) : ApiController(_serviceManager)
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _serviceManager.UserService.GetUsers();

        _response = new ApiResponse("Users", true, users, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);
    }
    [Authorize(Roles = "User")]
    [HttpGet("with-email/{email}")]
    public async Task<IActionResult> GetUserWithMail(string email)
    {
        var user = await _serviceManager.UserService.GetUserWithEmail(email);

        _response = new ApiResponse("User with Email", true, user, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);
    }

    [Authorize(Roles = "User")]
    [HttpGet("with-id/{id}")]
    public async Task<IActionResult> GetAuthorizedUserData(int id)
    {
        var user = await _serviceManager.UserService.GetAuthorizedUserData(id);

        _response = new ApiResponse("Authorized User Data", true, user, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);
    }
    [Authorize(Roles = "User")]
    [HttpPut("with-id/{id}")]
    public async Task<IActionResult> UpdateAuthorizedUserData(int id, [FromBody] AuthorizedUserDto authorizedUserDto)
    {
        await _serviceManager.UserService.UpdateAuthorizedUser(id, authorizedUserDto);

        _response = new ApiResponse("User Updated Succesfully", true, null, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{userId}/{ban}")]
    public async Task<IActionResult> BanUser(int userId ,Ban ban)
    {
        await _serviceManager.UserService.UserBanStatusChange(userId, ban);

        _response = new ApiResponse($"User {ban} Successfully", true, null, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);
    }


}
