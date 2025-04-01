#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermComment.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TermCommentUseCase.Commands.Active;

public class ActiveCommandHandler(
    IDateTime dateTime,
    ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser,
    ITermCommentCommandRepository termCommentCommandRepository
) : ICommandHandler<ActiveCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(ActiveCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    [WithCleanCache(Keies = "TermComments")]
    public async Task<string> HandleAsync(ActiveCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as TermComment;

        targetComment.Active(dateTime, identityUser, serializer);

        await termCommentCommandRepository.ChangeAsync(targetComment, cancellationToken);

        return targetComment.Id;
    }

    public Task AfterHandleAsync(ActiveCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}