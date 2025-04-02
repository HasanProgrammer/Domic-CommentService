#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Active;

public class ActiveCommandHandler(
    ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository,
    IDateTime dateTime,
    ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identity
) : ICommandHandler<ActiveCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(ActiveCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    [WithCleanCache(Keies = "TermComments")]
    public async Task<string> HandleAsync(ActiveCommand command, CancellationToken cancellationToken)
    {
        var answer = _validationResult as TermCommentAnswer;

        answer.Active(dateTime, identity, serializer);

        await termCommentAnswerCommandRepository.ChangeAsync(answer, cancellationToken);

        return answer.Id;
    }

    public Task AfterHandleAsync(ActiveCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}