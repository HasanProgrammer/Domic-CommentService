#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Update;

public class UpdateCommandHandler(
    IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository,
    IDateTime dateTime,
    ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser
) : ICommandHandler<UpdateCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(UpdateCommand command, CancellationToken cancellationToken) 
        => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(UpdateCommand command, CancellationToken cancellationToken)
    {
        var answer = _validationResult as ArticleCommentAnswer;

        answer.Change(dateTime, serializer, identityUser, command.Answer);

        await articleCommentAnswerCommandRepository.ChangeAsync(answer, cancellationToken);

        return answer.Id;
    }

    public Task AfterHandleAsync(UpdateCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}