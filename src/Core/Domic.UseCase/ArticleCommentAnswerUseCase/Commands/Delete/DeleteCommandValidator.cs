using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Delete;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.SoftDelete;

public class DeleteCommandValidator(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository) 
    : IValidator<DeleteCommand>
{
    public async Task<object> ValidateAsync(DeleteCommand input, CancellationToken cancellationToken)
    {
        var result = await articleCommentAnswerCommandRepository.FindByIdAsync(input.Id, cancellationToken);

        if (result is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.Id ?? "_خالی_")
            );

        return result;
    }
}