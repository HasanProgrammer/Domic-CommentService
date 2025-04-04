using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleComment.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.InActive;

public class InActiveCommandValidator(IArticleCommentCommandRepository articleCommentCommandRepository) : IValidator<InActiveCommand>
{   
    public async Task<object> ValidateAsync(InActiveCommand input, CancellationToken cancellationToken)
    {
        var targetComment = await articleCommentCommandRepository.FindByIdAsync(input.Id, cancellationToken);
        
        if (targetComment is null)
            throw new UseCaseException(
                string.Format("نظری با شناسه {0} یافت نشد !", input.Id ?? "_خالی_")
            );

        return targetComment;
    }
}