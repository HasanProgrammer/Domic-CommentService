#pragma warning disable CS0649

using Karami.Core.Domain.Enumerations;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Entities;
using Karami.UseCase.ArticleCommentAnswerUseCase.DTOs.ViewModels;
using Karami.UseCase.ArticleCommentUseCase.DTOs.ViewModels;

namespace Karami.UseCase.ArticleCommentUseCase.Queries.ReadOne;

public class ReadOneQueryHandler : IQueryHandler<ReadOneQuery, ArticleCommentsViewModel>
{
    private readonly object _validationResult;
    
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;

    public ReadOneQueryHandler(IArticleCommentCommandRepository articleCommentCommandRepository) 
        => _articleCommentCommandRepository = articleCommentCommandRepository;

    [WithValidation]
    public async Task<ArticleCommentsViewModel> HandleAsync(ReadOneQuery query, CancellationToken cancellationToken)
    {
        return await Task.Run(() => {
            
            var targetComment = _validationResult as ArticleComment;

            return new ArticleCommentsViewModel {
                Comment               = targetComment.Comment.Value               ,
                IsActive              = targetComment.IsActive == IsActive.Active ,
                CreatedAt_EnglishDate = targetComment.CreatedAt.EnglishDate       ,
                CreatedAt_PersianDate = targetComment.CreatedAt.PersianDate       ,
                UpdatedAt_EnglishDate = targetComment.UpdatedAt.EnglishDate       ,
                UpdatedAt_PersianDate = targetComment.UpdatedAt.PersianDate       ,
                Answers = targetComment.Answers.Select(answer => new ArticleCommentAnswersViewModel {
                    Answer                = answer.Answer.Value                ,
                    IsActive              = answer.IsActive == IsActive.Active , 
                    CreatedAt_EnglishDate = answer.CreatedAt.EnglishDate       ,
                    UpdatedAt_EnglishDate = answer.UpdatedAt.EnglishDate       ,
                    CreatedAt_PersianDate = answer.CreatedAt.PersianDate       ,
                    UpdatedAt_PersianDate = answer.UpdatedAt.PersianDate
                }).ToList()
            };

        });
    }
}