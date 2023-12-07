using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Commands.Create;

public class CreateCommand : ICommand<string>
{
    public required string Token     { get; set; }
    public required string OwnerId   { get; set; }
    public required string CommentId { get; set; }
    public required string Answer    { get; set; }
}