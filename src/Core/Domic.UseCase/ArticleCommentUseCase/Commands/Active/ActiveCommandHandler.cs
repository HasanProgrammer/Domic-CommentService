#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Active;

public class ActiveCommandHandler(
    IArticleCommentCommandRepository articleCommentCommandRepository, 
    IDateTime dateTime, 
    ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser
)  : ICommandHandler<ActiveCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(ActiveCommand command, CancellationToken cancellationToken) 
        => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(ActiveCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as ArticleComment;
        
        targetComment.Active(dateTime, identityUser, serializer);

        await articleCommentCommandRepository.ChangeAsync(targetComment, cancellationToken);

        return targetComment.Id;
    }

    public Task AfterHandleAsync(ActiveCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}