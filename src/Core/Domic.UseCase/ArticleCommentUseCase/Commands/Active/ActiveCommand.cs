using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Active;

public class ActiveCommand : ICommand<string>
{
    public required string Token     { get; set; }
    public required string TargetId  { get; set; }
}