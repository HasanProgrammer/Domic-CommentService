using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Enumerations;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.UseCase.ArticleCommentAnswerUseCase.DTOs.ViewModels;
using Karami.UseCase.ArticleCommentUseCase.DTOs.ViewModels;

namespace Karami.UseCase.ArticleCommentUseCase.Caches;

public class ArticlesEagerLoadingMemoryCache : IMemoryCacheSetter<List<ArticleCommentsViewModel>>
{
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;

    public ArticlesEagerLoadingMemoryCache(IArticleCommentCommandRepository articleCommentCommandRepository)
        => _articleCommentCommandRepository = articleCommentCommandRepository;

    [Config(Key = Cache.ArticleComments, Ttl = 30 * 24 * 60)]
    public async Task<List<ArticleCommentsViewModel>> SetAsync(CancellationToken cancellationToken)
    {
        var result =
            await _articleCommentCommandRepository.FindAllEagerLoadingWithOrderingByProjectionAsync<ArticleCommentsViewModel>(
                comment => new ArticleCommentsViewModel {
                    Comment               = comment.Comment.Value               ,
                    IsActive              = comment.IsActive == IsActive.Active ,
                    CreatedAt_EnglishDate = comment.CreatedAt.EnglishDate       ,
                    CreatedAt_PersianDate = comment.CreatedAt.PersianDate       ,
                    UpdatedAt_EnglishDate = comment.UpdatedAt.EnglishDate       ,
                    UpdatedAt_PersianDate = comment.UpdatedAt.PersianDate       ,
                    Answers = comment.Answers.Select(answer => new ArticleCommentAnswersViewModel {
                        Answer                = answer.Answer.Value                ,
                        IsActive              = answer.IsActive == IsActive.Active ,
                        CreatedAt_EnglishDate = answer.CreatedAt.EnglishDate       ,
                        CreatedAt_PersianDate = answer.CreatedAt.PersianDate       ,
                        UpdatedAt_EnglishDate = answer.UpdatedAt.EnglishDate       ,
                        UpdatedAt_PersianDate = answer.UpdatedAt.PersianDate
                    }).ToList()
                },
                Order.Date, false, cancellationToken
            );

        return result.ToList();
    }
}