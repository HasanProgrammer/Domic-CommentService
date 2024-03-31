using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Delete;

public class DeleteCommand : ICommand<string>
{
    public required string Token { get; set; }
    public required string TargetId { get; set; }
}