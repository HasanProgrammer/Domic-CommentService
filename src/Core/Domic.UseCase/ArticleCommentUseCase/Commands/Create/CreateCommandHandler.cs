using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Create;

public class CreateCommandHandler(
    IDateTime dateTime,
    ISerializer serializer,
    IArticleCommentCommandRepository articleCommentCommandRepository,
    IGlobalUniqueIdGenerator globalUniqueIdGenerator,
    [FromKeyedServices("Http2")] IIdentityUser identityUser
) : ICommandHandler<CreateCommand, string>
{
    public Task BeforeHandleAsync(CreateCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithTransaction]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var newComment = new ArticleComment(
            dateTime, globalUniqueIdGenerator, identityUser, serializer, command.ArticleId, command.Comment
        );

        await articleCommentCommandRepository.AddAsync(newComment, cancellationToken);

        return newComment.Id;
    }

    public Task AfterHandleAsync(CreateCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}