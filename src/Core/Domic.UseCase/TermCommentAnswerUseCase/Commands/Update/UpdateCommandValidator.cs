using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Update;

public class UpdateCommandValidator : IValidator<UpdateCommand>
{
    private readonly ITermCommentAnswerCommandRepository _repository;

    public UpdateCommandValidator(ITermCommentAnswerCommandRepository repository) => _repository = repository;

    public async Task<object> ValidateAsync(UpdateCommand input, CancellationToken cancellationToken)
    {
        var answer =
            await _repository.FindByIdAsync(input.AnswerId, cancellationToken);

        if (answer is null)
            throw new UseCaseException(string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.AnswerId));

        return answer;
    }
}