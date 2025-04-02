using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Active;

public class ActiveCommandValidator(ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository) : IValidator<ActiveCommand>
{
    public async Task<object> ValidateAsync(ActiveCommand input, CancellationToken cancellationToken)
    {
        var answer = await termCommentAnswerCommandRepository.FindByIdAsync(input.Id, cancellationToken);

        if (answer is null)
            throw new UseCaseException(
                string.Format("نظری با شناسه {0} یافت نشد !", input.Id ?? "_خالی_")
            );

        return answer;
    }
}