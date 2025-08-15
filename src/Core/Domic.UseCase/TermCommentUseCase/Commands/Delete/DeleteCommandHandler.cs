#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermComment.Entities;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TermCommentUseCase.Commands.Delete;

public class DeleteCommandHandler(
    IDateTime dateTime,
    ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser,
    ITermCommentCommandRepository termCommentCommandRepository,
    ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository
) : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;

    public Task BeforeHandleAsync(DeleteCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as TermComment;
        
        targetComment.Delete(dateTime, identityUser, serializer);

        await termCommentCommandRepository.ChangeAsync(targetComment, cancellationToken);

        var answers = new List<TermCommentAnswer>();

        foreach (var answer in targetComment.Answers)
        {
            answer.Delete(dateTime, identityUser, serializer);
            
            answers.Add(answer);
        }
        
        await termCommentAnswerCommandRepository.ChangeRangeAsync(answers, cancellationToken);

        return targetComment.Id;
    }

    public Task AfterHandleAsync(DeleteCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}