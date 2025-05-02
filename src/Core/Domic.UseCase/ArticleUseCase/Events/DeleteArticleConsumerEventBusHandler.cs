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

public class DeleteArticleConsumerEventBusHandler(
    IArticleCommentCommandRepository       articleCommentCommandRepository,
    IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository,
    IDateTime                              dateTime
) : IConsumerEventBusHandler<ArticleDeleted>
{
    public Task BeforeHandleAsync(ArticleDeleted @event, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [WithCleanCache(Keies = Cache.ArticleComments)]
    [TransactionConfig(Type = TransactionType.Command)]
    public async Task HandleAsync(ArticleDeleted @event, CancellationToken cancellationToken)
    {
        var comments = await articleCommentCommandRepository.FindAllEagerLoadingByArticleIdAsync(@event.Id, cancellationToken);

        var changedComments = new List<ArticleComment>();
        var changedAnswers = new List<ArticleCommentAnswer>();

        foreach (var comment in comments)
        {
            comment.Delete(dateTime, @event.UpdatedBy, @event.UpdatedRole, false);

            changedComments.Add(comment);
                        
            foreach (var answer in comment.Answers)
            {
                answer.Delete(dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
    
                changedAnswers.Add(answer);
            }
        }

        await articleCommentCommandRepository.ChangeRangeAsync(changedComments, cancellationToken);
        await articleCommentAnswerCommandRepository.ChangeRangeAsync(changedAnswers, cancellationToken);
    }

    public Task AfterHandleAsync(ArticleDeleted @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}