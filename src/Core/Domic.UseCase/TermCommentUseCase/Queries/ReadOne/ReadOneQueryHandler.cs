#pragma warning disable CS0649

using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermComment.Entities;
using Domic.UseCase.TermCommentUseCase.DTOs;

namespace Domic.UseCase.TermCommentUseCase.Queries.ReadOne;

public class ReadOneQueryHandler : IQueryHandler<ReadOneQuery, TermCommentsDto>
{
    private readonly object _validationResult;

    [WithValidation]
    public Task<TermCommentsDto> HandleAsync(ReadOneQuery query, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as TermComment;

        var dto = new TermCommentsDto {
            Comment               = targetComment.Comment.Value               ,
            IsActive              = targetComment.IsActive == IsActive.Active ,
            CreatedAt_EnglishDate = targetComment.CreatedAt.EnglishDate       ,
            CreatedAt_PersianDate = targetComment.CreatedAt.PersianDate       ,
            UpdatedAt_EnglishDate = targetComment.UpdatedAt.EnglishDate       ,
            UpdatedAt_PersianDate = targetComment.UpdatedAt.PersianDate       ,
            /*Answers = targetComment.Answers.Select(answer => new ArticleCommentAnswersViewModel {
                Answer                = answer.Answer.Value                ,
                IsActive              = answer.IsActive == IsActive.Active , 
                CreatedAt_EnglishDate = answer.CreatedAt.EnglishDate       ,
                UpdatedAt_EnglishDate = answer.UpdatedAt.EnglishDate       ,
                CreatedAt_PersianDate = answer.CreatedAt.PersianDate       ,
                UpdatedAt_PersianDate = answer.UpdatedAt.PersianDate
            }).ToList()*/
        };

        return Task.FromResult(dto);
    }
}