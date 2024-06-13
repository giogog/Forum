using Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IServiceManager _serviceManager) : ApiController(_serviceManager)
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var registrationCheckUp = await _serviceManager.AuthorizationService.Register(registerDto);

        if (!registrationCheckUp.Succeeded)
            return BadRequest(registrationCheckUp.Errors);

        var emailSent = await _serviceManager.EmailService.SendConfirmationMail(Url, registerDto.Username);

        if (!emailSent.IsSuccess)
            return BadRequest(emailSent.ErrorMessage);

        return Ok("Registration successful. Please check your email to confirm your account.");
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var loginCheckUp = await _serviceManager.AuthorizationService.Login(loginDto);

        if (!loginCheckUp.Succeeded)
            return BadRequest(loginCheckUp.Errors);


        return Ok(await _serviceManager.AuthorizationService.Authenticate(user => user.UserName == loginDto.Username));
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(int userId, string token)
    {
        var result = await _serviceManager.EmailService.ConfirmEmailAsync(userId.ToString(), token);

        if (!result.Succeeded)
        {
            return BadRequest("Error confirming email.");
        }

        return Ok(await _serviceManager.AuthorizationService.Authenticate(user => user.Id == userId));

    }

    [HttpPost("resend-confirmation")]
    public async Task<IActionResult> ResendConfirmation(string Username)
    {
        var alreadyConfirmed = await _serviceManager.EmailService.CheckMailConfirmation(Username);

        if (alreadyConfirmed.IsSuccess)
            return BadRequest(alreadyConfirmed.ErrorMessage);


        var emailSent = await _serviceManager.EmailService.SendConfirmationMail(Url, Username);

        if (!emailSent.IsSuccess)
            return BadRequest(emailSent.ErrorMessage);

        return Ok("Registration successful. Please check your email to confirm your account.");
    }


}
