using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentUseCase.Commands.Update;

public class UpdateCommand : ICommand<string>
{
    public required string Token    { get; set; }
    public required string TargetId { get; set; }
    public required string Comment  { get; set; }
}