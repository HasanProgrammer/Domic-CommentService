#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Delete;

public class DeleteCommandHandler(
    IDateTime dateTime,
    ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser,
    IArticleCommentCommandRepository articleCommentCommandRepository,
    IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository
) : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(DeleteCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as ArticleComment;
        
        targetComment.Delete(dateTime, identityUser, serializer);

        await articleCommentCommandRepository.ChangeAsync(targetComment, cancellationToken);

        foreach (var answer in targetComment.Answers)
        {
            answer.Delete(dateTime, serializer, identityUser);
            
            await articleCommentAnswerCommandRepository.ChangeAsync(answer, cancellationToken);
        }

        return targetComment.Id;
    }

    public Task AfterHandleAsync(DeleteCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}