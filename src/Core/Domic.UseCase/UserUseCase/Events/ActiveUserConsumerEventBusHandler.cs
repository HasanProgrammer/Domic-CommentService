using Domic.Core.Common.ClassConsts;
using Domic.Core.Common.ClassEnums;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Domic.Domain.ArticleCommentAnswer.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class ActiveUserConsumerEventBusHandler : IConsumerEventBusHandler<UserActived>
{
    private readonly IDateTime                              _dateTime;
    private readonly IArticleCommentCommandRepository       _articleCommentCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public ActiveUserConsumerEventBusHandler(IArticleCommentCommandRepository articleCommentCommandRepository,
        IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, IDateTime dateTime
    )
    {
        _dateTime                              = dateTime;
        _articleCommentCommandRepository       = articleCommentCommandRepository;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    public Task BeforeHandleAsync(UserActived @event, CancellationToken cancellationToken) => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Command)]
    [WithCleanCache(Keies = Cache.ArticleComments)]
    public async Task HandleAsync(UserActived @event, CancellationToken cancellationToken)
    {
        //active all user comments by answers
        
        var comments =
            await _articleCommentCommandRepository.FindAllEagerLoadingByOwnerIdAsync(@event.Id, cancellationToken);

        var articleComments = new List<ArticleComment>();
        var articleCommentAnswers = new List<ArticleCommentAnswer>();

        foreach (var comment in comments)
        {
            comment.Active(_dateTime, @event.UpdatedBy, @event.UpdatedRole, false);

            articleComments.Add(comment);

            foreach (var answer in comment.Answers)
            {
                answer.Active(_dateTime, @event.UpdatedBy, @event.UpdatedRole, false);

                articleCommentAnswers.Add(answer);
            }
        }

        await _articleCommentCommandRepository.ChangeRangeAsync(articleComments, cancellationToken);
        await _articleCommentAnswerCommandRepository.ChangeRangeAsync(articleCommentAnswers, cancellationToken);
        
        //active all user answers
        
        var answers = await _articleCommentAnswerCommandRepository.FindAllByOwnerIdAsync(@event.Id, cancellationToken);

        var userArticleCommentAnswers = new List<ArticleCommentAnswer>();

        foreach (var answer in answers)
        {
            answer.Active(_dateTime, @event.UpdatedBy, @event.UpdatedRole);

            userArticleCommentAnswers.Add(answer);
        }

        await _articleCommentAnswerCommandRepository.ChangeRangeAsync(userArticleCommentAnswers, cancellationToken);
    }

    public Task AfterHandleAsync(UserActived @event, CancellationToken cancellationToken) => Task.CompletedTask;
}