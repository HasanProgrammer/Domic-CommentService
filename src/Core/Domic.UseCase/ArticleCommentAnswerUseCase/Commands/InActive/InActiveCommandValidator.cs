using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.InActive;

public class InActiveCommandValidator : IValidator<InActiveCommand>
{
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public InActiveCommandValidator(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository) 
        => _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;

    public async Task<object> ValidateAsync(InActiveCommand input, CancellationToken cancellationToken)
    {
        var answer = await _articleCommentAnswerCommandRepository.FindByIdAsync(input.TargetId, cancellationToken);

        if (answer is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.TargetId ?? "_خالی_")
            );

        return answer;
    }
}