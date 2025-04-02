using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleComment.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Create;

public class CreateCommandValidator(IArticleCommentCommandRepository articleCommentCommandRepository) 
    : IValidator<CreateCommand>
{
    public async Task<object> ValidateAsync(CreateCommand input, CancellationToken cancellationToken)
    {
        if (!await articleCommentCommandRepository.IsExistByIdAsync(input.CommentId, cancellationToken))
            throw new UseCaseException(
                string.Format("نظری با شناسه {0} یافت نشد !", input.CommentId ?? "_خالی_")
            );

        return default;
    }
}