using Domic.Core.Common.ClassConsts;
using Domic.Core.Common.ClassEnums;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Article.Events;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;

namespace Domic.UseCase.ArticleUseCase.Events;

public class ActiveArticleConsumerEventBusHandler(
    IDateTime dateTime, 
    IArticleCommentCommandRepository articleCommentCommandRepository, 
    IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository
) : IConsumerEventBusHandler<ArticleActived>
{
    public Task BeforeHandleAsync(ArticleActived @event, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [WithCleanCache(Keies = Cache.ArticleComments)]
    [TransactionConfig(Type = TransactionType.Command)]
    public async Task HandleAsync(ArticleActived @event, CancellationToken cancellationToken)
    {
        var comments = await articleCommentCommandRepository.FindAllEagerLoadingByArticleIdAsync(@event.Id, cancellationToken);

        var changedComments = new List<ArticleComment>();
        var changedAnswers = new List<ArticleCommentAnswer>();

        foreach (var comment in comments)
        {
            comment.Active(dateTime, @event.UpdatedBy, @event.UpdatedRole, false);

            changedComments.Add(comment);
            
            foreach (var answer in comment.Answers)
            {
                answer.Active(dateTime, @event.UpdatedBy, @event.UpdatedRole, false);

                changedAnswers.Add(answer);
            }
        }

        await articleCommentCommandRepository.ChangeRangeAsync(changedComments, cancellationToken);
        await articleCommentAnswerCommandRepository.ChangeRangeAsync(changedAnswers, cancellationToken);
    }

    public Task AfterHandleAsync(ArticleActived @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}