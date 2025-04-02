using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.UseCase.TermCommentAnswerUseCase.Commands.CheckExist;
namespace Domic.UseCase.TermCommentAnswerUseCase.Queries.CheckExist;

public class CheckExistCommandHandler(ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository) : IQueryHandler<CheckExistCommand, bool>
{
    public async Task<bool> HandleAsync(CheckExistCommand command, CancellationToken cancellationToken) 
        => await termCommentAnswerCommandRepository.IsExistByIdAsync(command.Id, cancellationToken);
}