using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Term.Events;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.TermUseCase.Events;

public class ActiveTermConsumerEventBusHandler(IDateTime dateTime, 
    ITermCommentCommandRepository termCommentCommandRepository,
    ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository
)
: IConsumerEventBusHandler<TermActived>
{
    public Task BeforeHandleAsync(TermActived @event, CancellationToken cancellationToken) => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Command)]
    [WithCleanCache(Keies = "TermComments")]
    public async Task HandleAsync(TermActived @event, CancellationToken cancellationToken)
    {
        var comments =
            await termCommentCommandRepository.FindAllEagerLoadingByTermIdAsync(@event.Id, cancellationToken);

        foreach (var comment in comments)
        {
            comment.Active(dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
            
            termCommentCommandRepository.Change(comment);
            
            foreach (var answer in comment.Answers)
            {
                answer.Active(dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
                
                termCommentAnswerCommandRepository.Change(answer);
            }
        }
    }

    public Task AfterHandleAsync(TermActived @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}