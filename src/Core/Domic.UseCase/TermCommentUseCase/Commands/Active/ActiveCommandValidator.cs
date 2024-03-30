using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermComment.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Commands.Active;

public class ActiveCommandValidator : IValidator<ActiveCommand>
{
    private readonly ITermCommentCommandRepository _termCommentCommandRepository;

    public ActiveCommandValidator(ITermCommentCommandRepository termCommentCommandRepository) 
        => _termCommentCommandRepository = termCommentCommandRepository;

    public async Task<object> ValidateAsync(ActiveCommand input, CancellationToken cancellationToken)
    {
        var targetComment = await _termCommentCommandRepository.FindByIdAsync(input.CommentId, cancellationToken);
        
        if (targetComment is null)
            throw new UseCaseException(
                string.Format("فیلدی با شناسه {0} وجود خارجی ندارد !", input.CommentId ?? "_خالی_")
            );

        return targetComment;
    }
}