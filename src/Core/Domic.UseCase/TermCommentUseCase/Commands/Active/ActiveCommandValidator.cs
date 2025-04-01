using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermComment.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Commands.Active;

public class ActiveCommandValidator(ITermCommentCommandRepository termCommentCommandRepository) : IValidator<ActiveCommand>
{
    public async Task<object> ValidateAsync(ActiveCommand input, CancellationToken cancellationToken)
    {
        var targetComment = await termCommentCommandRepository.FindByIdAsync(input.Id, cancellationToken);
        
        if (targetComment is null)
            throw new UseCaseException(
                string.Format("کامنت با شناسه {0} یافت نشد !", input.Id ?? "_خالی_")
            );

        return targetComment;
    }
}