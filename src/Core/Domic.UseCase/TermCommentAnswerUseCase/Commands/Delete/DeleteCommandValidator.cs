using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.TermCommentAnswerUseCase.Commands.Delete;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.SoftDelete;

public class DeleteCommandValidator : IValidator<DeleteCommand>
{
    private readonly ITermCommentAnswerCommandRepository _repository;

    public DeleteCommandValidator(ITermCommentAnswerCommandRepository repository) => _repository = repository;

    public async Task<object> ValidateAsync(DeleteCommand input, CancellationToken cancellationToken)
    {
        var result = await _repository.FindByIdAsync(input.AnswerId, cancellationToken);

        if (result is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.AnswerId ?? "_خالی_")
            );

        return result;
    }
}