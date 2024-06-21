namespace Domain.Models;
public record CommentDto
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public string Body { get; set; }
    public string AuthorFullName { get; set; }
    public CommentDto()
    {

    }
}
public record CreateCommentDto(int TopicId, string Body);
public record UpdateCommentDto(string Body);