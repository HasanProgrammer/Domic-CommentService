using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Update;

public class UpdateCommandValidator(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository) : IValidator<UpdateCommand>
{
    public async Task<object> ValidateAsync(UpdateCommand input, CancellationToken cancellationToken)
    {
        var answer =
            await articleCommentAnswerCommandRepository.FindByIdAsync(input.Id, cancellationToken);

        if (answer is null)
            throw new UseCaseException(string.Format("موجودیتی با شناسه {0} یافت نشد !", input.Id));

        return answer;
    }
}