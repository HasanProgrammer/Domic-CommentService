using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Core.UseCase.Exceptions;
using Karami.Domain.ArticleComment.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentUseCase.Commands.Delete;

public class DeleteCommandValidator : IValidator<DeleteCommand>
{
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;

    public DeleteCommandValidator(IArticleCommentCommandRepository articleCommentCommandRepository) 
        => _articleCommentCommandRepository = articleCommentCommandRepository;

    public async Task<object> ValidateAsync(DeleteCommand input, CancellationToken cancellationToken)
    {
        var comment =
            await _articleCommentCommandRepository.FindByIdEagerLoadingAsync(input.TargetId, cancellationToken);

        if (comment is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.TargetId ?? "_خالی_")
            );

        return comment;
    }
}