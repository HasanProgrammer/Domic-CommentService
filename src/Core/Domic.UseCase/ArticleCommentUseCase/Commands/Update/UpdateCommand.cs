using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Update;

public class UpdateCommand : ICommand<string>
{
    public required string Token    { get; set; }
    public required string TargetId { get; set; }
    public required string Comment  { get; set; }
}