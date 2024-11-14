using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Term.Events;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.TermUseCase.Events;

public class DeleteTermConsumerEventBusHandler(
    IDateTime dateTime, 
    ITermCommentCommandRepository termCommentCommandRepository,
    ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository
)
: IConsumerEventBusHandler<TermDeleted>
{
    public void Handle(TermDeleted @event){}

    [TransactionConfig(Type = TransactionType.Command)]
    [WithCleanCache(Keies = "TermComments")]
    public async Task HandleAsync(TermDeleted @event, CancellationToken cancellationToken)
    {
        var comments = 
            await termCommentCommandRepository.FindAllEagerLoadingByTermIdAsync(@event.Id, cancellationToken);

        foreach (var comment in comments)
        {
            comment.Delete(dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
            
            termCommentCommandRepository.Change(comment);
            
            foreach (var answer in comment.Answers)
            {
                answer.Delete(dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
                
                termCommentAnswerCommandRepository.Change(answer);
            }
        }
    }

    public void AfterTransactionHandle(TermDeleted @event){}

    public Task AfterTransactionHandleAsync(TermDeleted @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}