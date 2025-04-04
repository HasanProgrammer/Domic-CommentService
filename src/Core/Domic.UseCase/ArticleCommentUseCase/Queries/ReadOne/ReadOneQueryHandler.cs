#pragma warning disable CS0649

using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Domic.UseCase.ArticleCommentAnswerUseCase.DTOs;
using Domic.UseCase.ArticleCommentUseCase.DTOs;

namespace Domic.UseCase.ArticleCommentUseCase.Queries.ReadOne;

public class ReadOneQueryHandler : IQueryHandler<ReadOneQuery, ArticleCommentDto>
{
    private readonly object _validationResult;

    [WithValidation]
    public Task<ArticleCommentDto> HandleAsync(ReadOneQuery query, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as ArticleComment;

        var result = new ArticleCommentDto {
            Comment               = targetComment.Comment.Value               ,
            IsActive              = targetComment.IsActive == IsActive.Active ,
            CreatedAt_EnglishDate = targetComment.CreatedAt.EnglishDate       ,
            CreatedAt_PersianDate = targetComment.CreatedAt.PersianDate       ,
            UpdatedAt_EnglishDate = targetComment.UpdatedAt.EnglishDate       ,
            UpdatedAt_PersianDate = targetComment.UpdatedAt.PersianDate       ,
            Answers = targetComment.Answers.Select(answer => new ArticleCommentAnswerDto {
                Answer                = answer.Answer.Value                ,
                IsActive              = answer.IsActive == IsActive.Active , 
                CreatedAt_EnglishDate = answer.CreatedAt.EnglishDate       ,
                UpdatedAt_EnglishDate = answer.UpdatedAt.EnglishDate       ,
                CreatedAt_PersianDate = answer.CreatedAt.PersianDate       ,
                UpdatedAt_PersianDate = answer.UpdatedAt.PersianDate
            }).ToList()
        };

        return Task.FromResult(result);
    }
}