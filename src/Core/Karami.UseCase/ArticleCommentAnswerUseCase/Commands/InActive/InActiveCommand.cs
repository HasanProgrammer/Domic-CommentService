using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Commands.InActive;

public class InActiveCommand : ICommand<string>
{
    public required string Token    { get; set; }
    public required string TargetId { get; set; }
}