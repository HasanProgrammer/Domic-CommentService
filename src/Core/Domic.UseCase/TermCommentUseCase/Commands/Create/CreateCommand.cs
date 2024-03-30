using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Commands.Create;

public class CreateCommand : Auditable, ICommand<string>
{
    public required string TermId { get; set; }
    public required string Comment { get; set; }
}