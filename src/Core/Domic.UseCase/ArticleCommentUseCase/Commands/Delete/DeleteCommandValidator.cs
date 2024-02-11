using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleComment.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Delete;

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