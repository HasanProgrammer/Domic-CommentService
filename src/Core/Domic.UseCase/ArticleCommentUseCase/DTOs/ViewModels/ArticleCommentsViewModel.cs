using Domic.UseCase.ArticleCommentAnswerUseCase.DTOs.ViewModels;
using Domic.Core.UseCase.DTOs.ViewModels;

namespace Domic.UseCase.ArticleCommentUseCase.DTOs.ViewModels;

public class ArticleCommentsViewModel : ViewModel
{
    public string Comment                  { get; set; }
    public bool IsActive                   { get; set; }
    public DateTime? CreatedAt_EnglishDate { get; set; }
    public DateTime? UpdatedAt_EnglishDate { get; set; }
    public string CreatedAt_PersianDate    { get; set; }
    public string UpdatedAt_PersianDate    { get; set; }
    
    public List<ArticleCommentAnswersViewModel> Answers { get; set; }
}