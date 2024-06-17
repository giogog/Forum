namespace Client.Models;

public record Result
{

}
public record RegisterResult : Result
{
    public bool Successful { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}


public record LoginResult : Result
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
}


public record UserResult:Result
{
    public int Id { get; init; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
public record AuthorizedUserResult : Result
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}