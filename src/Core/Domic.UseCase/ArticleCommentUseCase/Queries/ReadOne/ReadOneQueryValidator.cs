using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleComment.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Queries.ReadOne;

public class ReadOneQueryValidator(IArticleCommentCommandRepository articleCommentCommandRepository) : IValidator<ReadOneQuery>
{
    public async Task<object> ValidateAsync(ReadOneQuery input, CancellationToken cancellationToken)
    {
        var targetComment = await articleCommentCommandRepository.FindByIdEagerLoadingAsync(input.Id, cancellationToken);

        if (targetComment is null)
            throw new UseCaseException(
                string.Format("نظری با شناسه {0} یافت نشد !", input.Id ?? "_خالی_")
            );

        return targetComment;
    }
}