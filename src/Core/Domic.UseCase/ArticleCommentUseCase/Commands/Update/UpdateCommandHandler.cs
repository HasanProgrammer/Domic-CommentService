#pragma warning disable CS0649

using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Update;

public class UpdateCommandHandler(
    IDateTime dateTime, 
    ISerializer serializer, 
    IArticleCommentCommandRepository articleCommentCommandRepository,
    [FromKeyedServices("Http2")] IIdentityUser identityUser
) : ICommandHandler<UpdateCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(UpdateCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    [WithCleanCache(Keies = Cache.ArticleComments)]
    public async Task<string> HandleAsync(UpdateCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as ArticleComment;
        
        targetComment.Change(dateTime, identityUser, serializer, command.Comment);

        await articleCommentCommandRepository.ChangeAsync(targetComment, cancellationToken);

        return targetComment.Id;
    }

    public Task AfterHandleAsync(UpdateCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}