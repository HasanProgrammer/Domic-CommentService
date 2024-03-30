using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.UseCase.TermCommentUseCase.DTOs;

namespace Domic.UseCase.TermCommentUseCase.Caches;

public class TermsEagerLoadingMemoryCache(ITermCommentCommandRepository termCommentCommandRepository) 
    : IMemoryCacheSetter<List<TermCommentsDto>>
{
    [Config(Key = "TermComments", Ttl = 30 * 24 * 60)]
    public async Task<List<TermCommentsDto>> SetAsync(CancellationToken cancellationToken)
    {
        var result =
            await termCommentCommandRepository.FindAllEagerLoadingWithOrderingByProjectionAsync<TermCommentsDto>(
                comment => new TermCommentsDto {
                    Comment               = comment.Comment.Value               ,
                    IsActive              = comment.IsActive == IsActive.Active ,
                    CreatedAt_EnglishDate = comment.CreatedAt.EnglishDate       ,
                    CreatedAt_PersianDate = comment.CreatedAt.PersianDate       ,
                    UpdatedAt_EnglishDate = comment.UpdatedAt.EnglishDate       ,
                    UpdatedAt_PersianDate = comment.UpdatedAt.PersianDate       ,
                    /*Answers = comment.Answers.Select(answer => new ArticleCommentAnswersViewModel {
                        Answer                = answer.Answer.Value                ,
                        IsActive              = answer.IsActive == IsActive.Active ,
                        CreatedAt_EnglishDate = answer.CreatedAt.EnglishDate       ,
                        CreatedAt_PersianDate = answer.CreatedAt.PersianDate       ,
                        UpdatedAt_EnglishDate = answer.UpdatedAt.EnglishDate       ,
                        UpdatedAt_PersianDate = answer.UpdatedAt.PersianDate
                    }).ToList()*/
                },
                Order.Date, false, cancellationToken
            );

        return result.ToList();
    }
}