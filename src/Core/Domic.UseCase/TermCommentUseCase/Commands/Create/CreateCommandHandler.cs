using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermComment.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TermCommentUseCase.Commands.Create;

public class CreateCommandHandler(
    IDateTime dateTime,
    ISerializer serializer, 
    IGlobalUniqueIdGenerator globalUniqueIdGenerator, 
    [FromKeyedServices("Http2")] IIdentityUser identityUser,
    ITermCommentCommandRepository termCommentCommandRepository
) : ICommandHandler<CreateCommand, string>
{
    public Task BeforeHandleAsync(CreateCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithTransaction]
    [WithCleanCache(Keies = "TermComments")]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var newComment = new TermComment(
            dateTime, globalUniqueIdGenerator, identityUser, command.TermId, serializer, command.Comment
        );

        await termCommentCommandRepository.AddAsync(newComment, cancellationToken);

        return newComment.Id;
    }

    public Task AfterHandleAsync(CreateCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}