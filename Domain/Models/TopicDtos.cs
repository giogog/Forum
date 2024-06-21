namespace Domain.Models;
public record TopicWithContentDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Body { get; init; }
    public int CommentNum { get; init; }
    public DateTime Created { get; init; }
    public string Username {  get; init; }
    public string AuthorFullName { get; init; }
    public State State { get; init; }
    public Status Status { get; init; }
    public IEnumerable<CommentDto> Comments { get; init; }
    public TopicWithContentDto() { }
}

public record TopicDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public int CommentNum { get; init; }
    public DateTime Created { get; init; }
    public string Username { get; init; }
    public string AuthorFullName { get; init; }
    public State State { get; init; }
    public Status Status { get; init; }

    // Parameterless constructor
    public TopicDto() { }
}

public record CreateTopicDto(string Title, string Body);

public record UpdateTopicDto(string Title, string Body);
