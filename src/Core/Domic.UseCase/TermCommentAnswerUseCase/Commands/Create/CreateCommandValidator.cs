using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermComment.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Create;

public class CreateCommandValidator(ITermCommentCommandRepository termCommentCommandRepository) : IValidator<CreateCommand>
{
    public async Task<object> ValidateAsync(CreateCommand input, CancellationToken cancellationToken)
    {
        if (!await termCommentCommandRepository.IsExistByIdAsync(input.CommentId, cancellationToken))
            throw new UseCaseException(
                string.Format("نظری با شناسه {0} یافت نشد !", input.CommentId ?? "_خالی_")
            );

        return default;
    }
}