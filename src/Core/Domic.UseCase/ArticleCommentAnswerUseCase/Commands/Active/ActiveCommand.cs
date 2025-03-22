using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Active;

public class ActiveCommand : ICommand<string>
{
    public required string Id { get; set; }
}