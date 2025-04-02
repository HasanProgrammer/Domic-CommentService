#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Delete;

public class DeleteCommandHandler(
    IDateTime dateTime,
    ISerializer serializer,
    IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository,
    [FromKeyedServices("Http2")] IIdentityUser identityUser
) : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(DeleteCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetAnswer = _validationResult as ArticleCommentAnswer;
        
        targetAnswer.Delete(dateTime, serializer, identityUser);
        
        await articleCommentAnswerCommandRepository.ChangeAsync(targetAnswer, cancellationToken);

        return targetAnswer.Id;
    }

    public Task AfterHandleAsync(DeleteCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}