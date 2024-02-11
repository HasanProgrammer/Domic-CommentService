using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Delete;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.SoftDelete;

public class DeleteCommandValidator : IValidator<DeleteCommand>
{
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public DeleteCommandValidator(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository) 
        => _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;

    public async Task<object> ValidateAsync(DeleteCommand input, CancellationToken cancellationToken)
    {
        var result = await _articleCommentAnswerCommandRepository.FindByIdAsync(input.TargetId, cancellationToken);

        if (result is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.TargetId ?? "_خالی_")
            );

        return result;
    }
}