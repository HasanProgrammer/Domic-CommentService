using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermComment.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Commands.Update;

public class UpdateCommandValidator(ITermCommentCommandRepository termCommentCommandRepository) : IValidator<UpdateCommand>
{
    public async Task<object> ValidateAsync(UpdateCommand input, CancellationToken cancellationToken)
    {
        var targetComment = await termCommentCommandRepository.FindByIdAsync(input.Id, cancellationToken);

        if (targetComment is null)
            throw new UseCaseException(
                string.Format("کامنت با شناسه {0} یافت نشد !", input.Id ?? "_خالی_")
            );
        
        return targetComment;
    }
}