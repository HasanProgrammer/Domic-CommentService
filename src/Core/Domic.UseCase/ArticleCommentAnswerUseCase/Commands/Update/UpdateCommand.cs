using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Update;

public class UpdateCommand : ICommand<string>
{
    public required string Id { get; init; }
    public required string Answer { get; init; }
}