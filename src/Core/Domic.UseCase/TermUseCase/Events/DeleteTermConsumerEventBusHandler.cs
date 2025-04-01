using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Term.Events;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;

namespace Domic.UseCase.TermUseCase.Events;

public class DeleteTermConsumerEventBusHandler(
    IDateTime dateTime, 
    ITermCommentCommandRepository termCommentCommandRepository,
    ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository
)
: IConsumerEventBusHandler<TermDeleted>
{
    public Task BeforeHandleAsync(TermDeleted @event, CancellationToken cancellationToken) => Task.CompletedTask;

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

            var answers = new List<TermCommentAnswer>();
            
            foreach (var answer in comment.Answers)
            {
                answer.Delete(dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
                
                answers.Add(answer);
            }

            await termCommentAnswerCommandRepository.ChangeRangeAsync(answers, cancellationToken);
        }
    }

    public Task AfterHandleAsync(TermDeleted @event, CancellationToken cancellationToken)
        => Task.CompletedTask;
}