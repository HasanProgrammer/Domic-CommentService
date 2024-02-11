using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleComment.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Create;

public class CreateCommandValidator : IValidator<CreateCommand>
{
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;

    public CreateCommandValidator(IArticleCommentCommandRepository articleCommentCommandRepository) 
        => _articleCommentCommandRepository = articleCommentCommandRepository;

    public async Task<object> ValidateAsync(CreateCommand input, CancellationToken cancellationToken)
    {
        var targetComment = await _articleCommentCommandRepository.FindByIdAsync(input.CommentId, cancellationToken);

        if (targetComment is null)
            throw new UseCaseException(
                string.Format("نظری با شناسه {0} وجود خارجی ندارد !", input.CommentId ?? "_خالی_")
            );

        return default;
    }
}