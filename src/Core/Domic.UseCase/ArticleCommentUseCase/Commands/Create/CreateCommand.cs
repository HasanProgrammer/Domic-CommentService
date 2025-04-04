using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Create;

public class CreateCommand : ICommand<string>
{
    public required string ArticleId { get; set; }
    public required string Comment   { get; set; }
}