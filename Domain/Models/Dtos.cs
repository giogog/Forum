﻿namespace Domain.Models;

//Account
public record LoginDto(string Username, string Password);


public record RegisterDto(string Name, string Surname, string Username, string Email, string Password);
public record LoginResponseDto(int id,string Username, string Token);
//Topics
public record TopicWithContentDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Body { get; init; }
    public int CommentNum { get; init; }
    public DateTime Created { get; init; }
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
    public string AuthorFullName { get; init; }
    public State State { get; init; }
    public Status Status { get; init; }

    // Parameterless constructor
    public TopicDto() { }
}

public record CreateTopicDto(string Title, string Body);

public record UpdateTopicDto(string Title, string Body);

//Comment

public record CommentDto
{
    public int Id { get; init; }
    public string Body { get; set; }
    public string AuthorFullName { get; set; }
    public CommentDto()
    {

    }
}
public record CreateCommentDto(int TopicId, string Body);
public record UpdateCommentDto(string Body);

//User

public record UserDto
{
    public int Id { get; init; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}






