#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermComment.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TermCommentUseCase.Commands.Update;

public class UpdateCommandHandler(
    IDateTime dateTime, 
    ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser,
    ITermCommentCommandRepository termCommentCommandRepository
) : ICommandHandler<UpdateCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(UpdateCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    [WithCleanCache(Keies = "TermComments")]
    public async Task<string> HandleAsync(UpdateCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as TermComment;

        targetComment.Change(dateTime, identityUser, serializer, command.Comment);

        await termCommentCommandRepository.ChangeAsync(targetComment, cancellationToken);

        return targetComment.Id;
    }

    public Task AfterHandleAsync(UpdateCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}