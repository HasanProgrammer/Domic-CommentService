using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentUseCase.Commands.Active;

public class ActiveCommand : ICommand<string>
{
    public required string Token     { get; set; }
    public required string TargetId  { get; set; }
}