using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermComment.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Commands.Delete;

public class DeleteCommandValidator : IValidator<DeleteCommand>
{
    private readonly ITermCommentCommandRepository _termCommentCommandRepository;

    public DeleteCommandValidator(ITermCommentCommandRepository termCommentCommandRepository) 
        => _termCommentCommandRepository = termCommentCommandRepository;

    public async Task<object> ValidateAsync(DeleteCommand input, CancellationToken cancellationToken)
    {
        var comment =
            await _termCommentCommandRepository.FindByIdEagerLoadingAsync(input.CommentId, cancellationToken);

        if (comment is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.CommentId ?? "_خالی_")
            );

        return comment;
    }
}