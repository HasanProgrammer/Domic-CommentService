using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Create;

public class CreateCommand : ICommand<string>
{
    public required string Answer { get; set; }
    public required string CommentId { get; set; }
}