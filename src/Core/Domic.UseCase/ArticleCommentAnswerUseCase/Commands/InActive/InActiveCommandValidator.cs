using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.InActive;

public class InActiveCommandValidator(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository) 
    : IValidator<InActiveCommand>
{
    public async Task<object> ValidateAsync(InActiveCommand input, CancellationToken cancellationToken)
    {
        var answer = await articleCommentAnswerCommandRepository.FindByIdAsync(input.Id, cancellationToken);

        if (answer is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.Id ?? "_خالی_")
            );

        return answer;
    }
}