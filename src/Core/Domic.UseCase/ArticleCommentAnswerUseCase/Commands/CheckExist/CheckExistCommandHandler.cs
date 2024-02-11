using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.CheckExist;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Queries.CheckExist;

public class CheckExistCommandHandler : IQueryHandler<CheckExistCommand, bool>
{
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public CheckExistCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository) 
        => _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;

    public async Task<bool> HandleAsync(CheckExistCommand command, CancellationToken cancellationToken)
    {
        var result = await _articleCommentAnswerCommandRepository.FindByIdAsync(command.AnswerId, cancellationToken);

        return result is not null;
    }
}