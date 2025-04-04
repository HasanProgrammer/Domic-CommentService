using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.TermCommentAnswerUseCase.Commands.InActive;

namespace Domic.UseCase.TermeCommentAnswerUseCase.Commands.InActive;

public class InActiveCommandValidator(ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository) : IValidator<InActiveCommand>
{
    public async Task<object> ValidateAsync(InActiveCommand input, CancellationToken cancellationToken)
    {
        var answer = await termCommentAnswerCommandRepository.FindByIdAsync(input.Id, cancellationToken);

        if (answer is null)
            throw new UseCaseException(
                string.Format("پاسخی با شناسه {0} یافت نشد !", input.Id ?? "_خالی_")
            );

        return answer;
    }
}