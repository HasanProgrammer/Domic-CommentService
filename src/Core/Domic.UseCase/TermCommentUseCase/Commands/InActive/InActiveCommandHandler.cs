#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermComment.Entities;

namespace Domic.UseCase.TermCommentUseCase.Commands.InActive;

public class InActiveCommandHandler : ICommandHandler<InActiveCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime _dateTime;
    private readonly ISerializer _serializer;
    private readonly ITermCommentCommandRepository _termCommentCommandRepository;

    public InActiveCommandHandler(ITermCommentCommandRepository termCommentCommandRepository, IDateTime dateTime,
        ISerializer serializer
    )
    {
        _dateTime = dateTime;
        _serializer = serializer;
        _termCommentCommandRepository = termCommentCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(InActiveCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as TermComment;

        targetComment.InActive(_dateTime, command.UserId, _serializer.Serialize(command.UserRoles));

        _termCommentCommandRepository.Change(targetComment);

        return Task.FromResult(targetComment.Id);
    }

    public Task AfterTransactionHandleAsync(InActiveCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}