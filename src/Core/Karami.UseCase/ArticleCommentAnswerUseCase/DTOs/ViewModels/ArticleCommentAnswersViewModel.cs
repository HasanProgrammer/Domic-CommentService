using Karami.Core.UseCase.DTOs.ViewModels;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.DTOs.ViewModels;

public class ArticleCommentAnswersViewModel : ViewModel
{
    public string Answer                   { get; set; }
    public bool IsActive                   { get; set; }
    public DateTime? CreatedAt_EnglishDate { get; set; }
    public DateTime? UpdatedAt_EnglishDate { get; set; }
    public string CreatedAt_PersianDate    { get; set; }
    public string UpdatedAt_PersianDate    { get; set; }
}