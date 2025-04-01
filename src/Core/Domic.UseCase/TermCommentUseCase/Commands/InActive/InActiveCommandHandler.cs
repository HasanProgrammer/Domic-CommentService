#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermComment.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TermCommentUseCase.Commands.InActive;

public class InActiveCommandHandler(
    IDateTime dateTime,
    ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser,
    ITermCommentCommandRepository termCommentCommandRepository
) : ICommandHandler<InActiveCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(InActiveCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    [WithCleanCache(Keies = "TermComments")]
    public async Task<string> HandleAsync(InActiveCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as TermComment;

        targetComment.InActive(dateTime, identityUser, serializer);

        await termCommentCommandRepository.ChangeAsync(targetComment, cancellationToken);

        return targetComment.Id;
    }

    public Task AfterHandleAsync(InActiveCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}