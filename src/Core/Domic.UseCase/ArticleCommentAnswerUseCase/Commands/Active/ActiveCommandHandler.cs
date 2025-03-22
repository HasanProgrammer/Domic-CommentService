#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Active;

public class ActiveCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, 
    IDateTime dateTime, ISerializer serializer, [FromKeyedServices("Http2")] IIdentityUser identityUser
) : ICommandHandler<ActiveCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(ActiveCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(ActiveCommand command, CancellationToken cancellationToken)
    {
        var answer = _validationResult as ArticleCommentAnswer;

        answer.Active(dateTime, serializer, identityUser);

        await articleCommentAnswerCommandRepository.ChangeAsync(answer, cancellationToken);

        return answer.Id;
    }

    public Task AfterHandleAsync(ActiveCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}