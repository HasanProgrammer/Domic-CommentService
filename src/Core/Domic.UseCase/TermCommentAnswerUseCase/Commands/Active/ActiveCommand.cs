using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Active;

public class ActiveCommand : Auditable, ICommand<string>
{
    public required string AnswerId { get; set; }
}