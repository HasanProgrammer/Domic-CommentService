using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Commands.Delete;

public class DeleteCommand : Auditable, ICommand<string>
{
    public required string CommentId { get; set; }
}