using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Commands.Update;

public class UpdateCommand : Auditable, ICommand<string>
{
    public required string CommentId { get; set; }
    public required string Comment { get; set; }
}