using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Delete;

public class DeleteCommand : ICommand<string>
{
    public required string Id { get; set; }
}