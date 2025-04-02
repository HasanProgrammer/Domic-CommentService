using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Active;

public class ActiveCommandValidator(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository) 
    : IValidator<ActiveCommand>
{
    public async Task<object> ValidateAsync(ActiveCommand input, CancellationToken cancellationToken)
    {
        var answer = await articleCommentAnswerCommandRepository.FindByIdAsync(input.Id, cancellationToken);

        if (answer is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} یافت نشد !", input.Id ?? "_خالی_")
            );

        return answer;
    }
}