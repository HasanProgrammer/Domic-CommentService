using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Commands.Update;

public class UpdateCommand : ICommand<string>
{
    public required string Id { get; set; }
    public required string Comment { get; set; }
}