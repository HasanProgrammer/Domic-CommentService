using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.TermCommentAnswerUseCase.Commands.Delete;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.SoftDelete;

public class DeleteCommandValidator(ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository) : IValidator<DeleteCommand>
{
    public async Task<object> ValidateAsync(DeleteCommand input, CancellationToken cancellationToken)
    {
        var targetAnswer = await termCommentAnswerCommandRepository.FindByIdAsync(input.Id, cancellationToken);

        if (targetAnswer is null)
            throw new UseCaseException(
                string.Format("نظری با شناسه {0} یافت نشد !", input.Id ?? "_خالی_")
            );

        return targetAnswer;
    }
}