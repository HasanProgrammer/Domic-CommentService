using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Core.UseCase.Exceptions;
using Karami.Domain.ArticleComment.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentUseCase.Queries.ReadOne;

public class ReadOneQueryValidator : IValidator<ReadOneQuery>
{
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;

    public ReadOneQueryValidator(IArticleCommentCommandRepository articleCommentCommandRepository) 
        => _articleCommentCommandRepository = articleCommentCommandRepository;

    public async Task<object> ValidateAsync(ReadOneQuery input, CancellationToken cancellationToken)
    {
        var targetComment =
            await _articleCommentCommandRepository.FindByIdEagerLoadingAsync(input.Id, cancellationToken);

        if (targetComment is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.Id ?? "_خالی_")
            );

        return targetComment;
    }
}