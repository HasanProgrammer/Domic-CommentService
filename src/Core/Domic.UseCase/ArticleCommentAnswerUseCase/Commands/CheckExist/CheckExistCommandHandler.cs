using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.CheckExist;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Queries.CheckExist;

public class CheckExistCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository) 
    : IQueryHandler<CheckExistCommand, bool>
{
    public Task<bool> HandleAsync(CheckExistCommand command, CancellationToken cancellationToken) 
        => articleCommentAnswerCommandRepository.IsExistByIdAsync(command.Id, cancellationToken);
}