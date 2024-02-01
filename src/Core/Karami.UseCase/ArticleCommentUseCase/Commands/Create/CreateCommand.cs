using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentUseCase.Commands.Create;

public class CreateCommand : ICommand<string>
{
    public required string Token     { get; set; }
    public required string ArticleId { get; set; }
    public required string Comment   { get; set; }
}