using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Create;

public class CreateCommandHandler(
    IDateTime dateTime,
    ISerializer serializer,
    IGlobalUniqueIdGenerator globalUniqueIdGenerator,
    IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository,
    [FromKeyedServices("Http2")] IIdentityUser identityUser
) : ICommandHandler<CreateCommand, string>
{
    public Task BeforeHandleAsync(CreateCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var newAnswer = new ArticleCommentAnswer(
            globalUniqueIdGenerator, dateTime, serializer, identityUser, command.CommentId, command.Answer
        );

        await articleCommentAnswerCommandRepository.AddAsync(newAnswer, cancellationToken);

        return newAnswer.Id;
    }

    public Task AfterHandleAsync(CreateCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}