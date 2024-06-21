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

public record TopicWithContentResult : Result
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Body { get; init; }
    public int CommentNum { get; init; }
    public DateTime Created { get; init; }
    public string AuthorFullName { get; init; }
    public State State { get; init; }
    public Status Status { get; init; }
    public List<CommentResult> Comments { get; init; }
}
public record CommentResult : Result
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public string Body { get; set; }
    public string AuthorFullName { get; set; }
}

public class PagedList<T>
{
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        SelectedPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        ItemCount = count;
        PageSize = pageSize;
        Data = items;
    }
    public IEnumerable<T> Data { get; set; }
    public int SelectedPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int ItemCount { get; set; }
}