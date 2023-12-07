using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Core.UseCase.Exceptions;
using Karami.Domain.ArticleComment.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Commands.Create;

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