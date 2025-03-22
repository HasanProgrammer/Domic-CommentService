using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Create;

public class CreateCommand : ICommand<string>
{
    public required string CommentId { get; set; }
    public required string Answer    { get; set; }
}