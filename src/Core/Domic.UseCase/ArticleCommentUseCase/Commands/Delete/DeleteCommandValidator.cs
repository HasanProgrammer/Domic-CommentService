using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleComment.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Delete;

public class DeleteCommandValidator(IArticleCommentCommandRepository articleCommentCommandRepository) : IValidator<DeleteCommand>
{
    public async Task<object> ValidateAsync(DeleteCommand input, CancellationToken cancellationToken)
    {
        var comment = await articleCommentCommandRepository.FindByIdEagerLoadingAsync(input.Id, cancellationToken);

        if (comment is null)
            throw new UseCaseException(
                string.Format("نظری با شناسه {0} یافت نشد !", input.Id ?? "_خالی_")
            );

        return comment;
    }
}