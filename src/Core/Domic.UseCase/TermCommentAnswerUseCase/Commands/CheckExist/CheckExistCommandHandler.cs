using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.CheckExist;

namespace Domic.UseCase.TermCommentAnswerUseCase.Queries.CheckExist;

public class CheckExistCommandHandler : IQueryHandler<CheckExistCommand, bool>
{
    private readonly ITermCommentAnswerCommandRepository _termCommentAnswerCommandRepository;

    public CheckExistCommandHandler(ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository) 
        => _termCommentAnswerCommandRepository = termCommentAnswerCommandRepository;

    public async Task<bool> HandleAsync(CheckExistCommand command, CancellationToken cancellationToken)
    {
        var result = await _termCommentAnswerCommandRepository.FindByIdAsync(command.AnswerId, cancellationToken);

        return result is not null;
    }
}