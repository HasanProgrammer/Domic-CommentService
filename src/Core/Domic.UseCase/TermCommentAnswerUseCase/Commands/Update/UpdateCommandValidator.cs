using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Update;

public class UpdateCommandValidator(ITermCommentAnswerCommandRepository repository) : IValidator<UpdateCommand>
{
    public async Task<object> ValidateAsync(UpdateCommand input, CancellationToken cancellationToken)
    {
        var answer = await repository.FindByIdAsync(input.AnswerId, cancellationToken);

        if (answer is null)
            throw new UseCaseException(string.Format("پاسخی با شناسه {0} یافت نشد !", input.AnswerId));

        return answer;
    }
}