using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Commands.Active;

public class ActiveCommand : Auditable, ICommand<string>
{
    public required string CommentId  { get; set; }
}