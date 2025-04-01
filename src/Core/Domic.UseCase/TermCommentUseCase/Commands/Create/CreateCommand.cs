using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Commands.Create;

public class CreateCommand : ICommand<string>
{
    public required string TermId { get; set; }
    public required string Comment { get; set; }
}