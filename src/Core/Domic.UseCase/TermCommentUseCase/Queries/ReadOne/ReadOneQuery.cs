using Domic.UseCase.ArticleCommentUseCase.DTOs.ViewModels;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Queries.ReadOne;

public class ReadOneQuery : IQuery<ArticleCommentsViewModel>
{
    public string Id { get; set; }
}