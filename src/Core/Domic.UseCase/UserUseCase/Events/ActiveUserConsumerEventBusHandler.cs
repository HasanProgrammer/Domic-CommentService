using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
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

    public Task BeforeHandleAsync(UserActived @event, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    [TransactionConfig(Type = TransactionType.Command)]
    [WithCleanCache(Keies = Cache.ArticleComments)]
    public Task HandleAsync(UserActived @event, CancellationToken cancellationToken)
    {
        //active all user comments by answers
        
        var comments =
            await_articleCommentCommandRepository.FindAllEagerLoadingByOwnerIdAsync(@event.Id, cancellationToken);

        foreach (var comment in comments)
        {
            comment.Active(_dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
            
            await _articleCommentCommandRepository.ChangeAsync(comment, cancellationToken);

            foreach (var answer in comment.Answers)
            {
                answer.Active(_dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
                
                await _articleCommentAnswerCommandRepository.ChangeAsync(answer, cancellationToken);
            }
        }
        
        //Active all user answers
        
        var answers = await _articleCommentAnswerCommandRepository.FindAllByOwnerIdAsync(@event.Id, cancellationToken);

        foreach (var answer in answers)
        {
            answer.Active(_dateTime, @event.UpdatedBy, @event.UpdatedRole);

            await _articleCommentAnswerCommandRepository.ChangeAsync(answer, cancellationToken);
        }
    }

    public Task AfterHandleAsync(UserActived @event, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}