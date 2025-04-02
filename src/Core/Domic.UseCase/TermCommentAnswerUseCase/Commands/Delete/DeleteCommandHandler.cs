#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Delete;

public class DeleteCommandHandler(
    ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository,
    IDateTime dateTime,
    ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser
) : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(DeleteCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    [WithCleanCache(Keies = "TermComments")]
    public async Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetAnswer = _validationResult as TermCommentAnswer;

        targetAnswer.Delete(dateTime, identityUser, serializer);

        await termCommentAnswerCommandRepository.ChangeAsync(targetAnswer, cancellationToken);

        return targetAnswer.Id;
    }

    public Task AfterTransactionHandleAsync(DeleteCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}