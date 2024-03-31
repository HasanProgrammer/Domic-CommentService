using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermComment.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Create;

public class CreateCommandValidator : IValidator<CreateCommand>
{
    private readonly ITermCommentCommandRepository _repository;

    public CreateCommandValidator(ITermCommentCommandRepository repository) => _repository = repository;

    public async Task<object> ValidateAsync(CreateCommand input, CancellationToken cancellationToken)
    {
        var targetComment = await _repository.FindByIdAsync(input.CommentId, cancellationToken);

        if (targetComment is null)
            throw new UseCaseException(
                string.Format("نظری با شناسه {0} وجود خارجی ندارد !", input.CommentId ?? "_خالی_")
            );

        return default;
    }
}