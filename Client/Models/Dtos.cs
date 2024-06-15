using System.ComponentModel.DataAnnotations;

namespace Client.Models;

public class ApiRequest(ApiType type, string endpoint, object data)
{
    public ApiType ApiType { get; set; } = type;
    public string Endpoint { get; set; } = endpoint;
    public object Data { get; set; } = data;
    public string? AccessToken { get; set; }
}


public record ApiResponse<T>
{
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
    public T? Result { get; set; }
    public int StatusCode { get; set; }
}

public record LoginModel
{
    [Required]
    public string Username { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";
}

public record PassowrdRenewalRequestDto
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }
}
public record PasswordResetModelDto
{
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
}
public record ResetPasswordDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
}
public record RegisterModelDto
{
    [Display(Name = "Name")]
    public string? Name { get; set; }
    [Display(Name = "Surname")]
    public string? Surname { get; set; }
    [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [Display(Name = "Username")]
    public string? Username { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
}

public record Result
{

}
public record RegisterResult : Result
{
    public bool Successful { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}


public record LoginResult:Result
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
}








