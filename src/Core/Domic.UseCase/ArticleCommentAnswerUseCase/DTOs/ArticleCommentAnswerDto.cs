namespace Domic.UseCase.ArticleCommentAnswerUseCase.DTOs;

public class ArticleCommentAnswerDto
{
    public string Answer                   { get; set; }
    public bool IsActive                   { get; set; }
    public DateTime? CreatedAt_EnglishDate { get; set; }
    public DateTime? UpdatedAt_EnglishDate { get; set; }
    public string CreatedAt_PersianDate    { get; set; }
    public string UpdatedAt_PersianDate    { get; set; }
}