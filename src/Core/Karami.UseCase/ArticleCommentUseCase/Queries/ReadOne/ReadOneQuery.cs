using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.UseCase.ArticleCommentUseCase.DTOs.ViewModels;

namespace Karami.UseCase.ArticleCommentUseCase.Queries.ReadOne;

public class ReadOneQuery : IQuery<ArticleCommentsViewModel>
{
    public string Id { get; set; }
}