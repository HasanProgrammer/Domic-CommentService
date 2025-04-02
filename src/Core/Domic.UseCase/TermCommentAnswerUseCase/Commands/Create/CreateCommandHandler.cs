using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Create;

public class CreateCommandHandler(
    IDateTime dateTime,
    ISerializer serializer,
    IGlobalUniqueIdGenerator globalUniqueIdGenerator,
    [FromKeyedServices("Http2")] IIdentityUser identityUser,
    ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository
) : ICommandHandler<CreateCommand, string>
{
    public Task BeforeHandleAsync(CreateCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    [WithCleanCache(Keies = "TermComments")]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var newAnswer = new TermCommentAnswer(
            dateTime, globalUniqueIdGenerator, identityUser, serializer, command.CommentId, command.Answer
        );  

        await termCommentAnswerCommandRepository.AddAsync(newAnswer, cancellationToken);

        return newAnswer.Id;
    }

    public Task AfterHandleAsync(CreateCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}