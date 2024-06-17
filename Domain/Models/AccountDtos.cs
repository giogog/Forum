namespace Domain.Models;

public record LoginDto(string Username, string Password);


public record RegisterDto(string Name, string Surname, string Username, string Email, string Password);
public record LoginResponseDto(int id, string Username, string Token);

public record ResetPasswordDto(string email, string token, string NewPassword);

