using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.InActive;

public class InActiveCommand : ICommand<string>
{
    public required string Token    { get; set; }
    public required string TargetId { get; set; }
}