using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentUseCase.Commands.Delete;

public class DeleteCommand : ICommand<string>
{
    public required string Token    { get; set; }
    public required string TargetId { get; set; }
}