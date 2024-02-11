using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleComment.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Active;

public class ActiveCommandValidator : IValidator<ActiveCommand>
{
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;

    public ActiveCommandValidator(IArticleCommentCommandRepository articleCommentCommandRepository) 
        => _articleCommentCommandRepository = articleCommentCommandRepository;

    public async Task<object> ValidateAsync(ActiveCommand input, CancellationToken cancellationToken)
    {
        var targetComment = await _articleCommentCommandRepository.FindByIdAsync(input.TargetId, cancellationToken);
        
        if (targetComment is null)
            throw new UseCaseException(
                string.Format("فیلدی با شناسه {0} وجود خارجی ندارد !", input.TargetId ?? "_خالی_")
            );

        return targetComment;
    }
}