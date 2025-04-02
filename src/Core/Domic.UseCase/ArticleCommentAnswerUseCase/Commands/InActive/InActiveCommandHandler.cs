#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.InActive;

public class InActiveCommandHandler(
    IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository,
    IDateTime dateTime,
    ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser
) : ICommandHandler<InActiveCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(InActiveCommand command, CancellationToken cancellationToken) 
        => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(InActiveCommand command, CancellationToken cancellationToken)
    {
        var answer = _validationResult as ArticleCommentAnswer;
        
        answer.InActive(dateTime, serializer, identityUser);

        await articleCommentAnswerCommandRepository.ChangeAsync(answer, cancellationToken);

        return answer.Id;
    }

    public Task AfterHandleAsync(InActiveCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}