#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.InActive;

public class InActiveCommandHandler(
    ITermCommentAnswerCommandRepository repository, 
    IDateTime dateTime, 
    ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser
) : ICommandHandler<InActiveCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(InActiveCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(InActiveCommand command, CancellationToken cancellationToken)
    {
        var answer = _validationResult as TermCommentAnswer;

        answer.InActive(dateTime, identityUser, serializer);

        await repository.ChangeAsync(answer, cancellationToken);

        return answer.Id;
    }

    public Task AfterHandleAsync(InActiveCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}