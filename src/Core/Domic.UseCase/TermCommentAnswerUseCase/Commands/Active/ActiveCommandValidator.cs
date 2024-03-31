using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Active;

public class ActiveCommandValidator : IValidator<ActiveCommand>
{
    private readonly ITermCommentAnswerCommandRepository _termCommentAnswerCommandRepository;

    public ActiveCommandValidator(ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository)
        => _termCommentAnswerCommandRepository = termCommentAnswerCommandRepository;

    public async Task<object> ValidateAsync(ActiveCommand input, CancellationToken cancellationToken)
    {
        var answer = await _termCommentAnswerCommandRepository.FindByIdAsync(input.AnswerId, cancellationToken);

        if (answer is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.AnswerId ?? "_خالی_")
            );

        return answer;
    }
}