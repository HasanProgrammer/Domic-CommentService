using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Update;

public class UpdateCommand : Auditable, ICommand<string>
{
    public required string Answer { get; init; }
    public required string AnswerId { get; init; }
}