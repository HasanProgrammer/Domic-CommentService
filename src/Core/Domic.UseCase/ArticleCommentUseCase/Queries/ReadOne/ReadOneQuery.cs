using Domic.UseCase.ArticleCommentUseCase.DTOs;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Queries.ReadOne;

public class ReadOneQuery : IQuery<ArticleCommentDto>
{
    public string Id { get; set; }
}