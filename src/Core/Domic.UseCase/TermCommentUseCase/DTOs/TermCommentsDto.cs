namespace Domic.UseCase.TermCommentUseCase.DTOs;

public class TermCommentsDto
{
    public string Comment                  { get; set; }
    public bool IsActive                   { get; set; }
    public DateTime? CreatedAt_EnglishDate { get; set; }
    public DateTime? UpdatedAt_EnglishDate { get; set; }
    public string CreatedAt_PersianDate    { get; set; }
    public string UpdatedAt_PersianDate    { get; set; }
    
    //public List<TermCommentAnswersDto> Answers { get; set; }
}