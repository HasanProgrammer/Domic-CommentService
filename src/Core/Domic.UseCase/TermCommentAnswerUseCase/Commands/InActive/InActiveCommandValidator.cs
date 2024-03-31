using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.TermCommentAnswerUseCase.Commands.InActive;

namespace Domic.UseCase.TermeCommentAnswerUseCase.Commands.InActive;

public class InActiveCommandValidator : IValidator<InActiveCommand>
{
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public InActiveCommandValidator(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository) 
        => _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;

    public async Task<object> ValidateAsync(InActiveCommand input, CancellationToken cancellationToken)
    {
        var answer = await _articleCommentAnswerCommandRepository.FindByIdAsync(input.AnswerId, cancellationToken);

        if (answer is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.AnswerId ?? "_خالی_")
            );

        return answer;
    }
}