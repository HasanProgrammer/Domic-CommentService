#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermComment.Entities;

namespace Domic.UseCase.TermCommentUseCase.Commands.Update;

public class UpdateCommandHandler : ICommandHandler<UpdateCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime _dateTime;
    private readonly ISerializer _serializer;
    private readonly ITermCommentCommandRepository _termCommentCommandRepository;

    public UpdateCommandHandler(ITermCommentCommandRepository termCommentCommandRepository,
        IDateTime dateTime, ISerializer serializer
    )
    {
        _dateTime = dateTime;
        _serializer = serializer;
        _termCommentCommandRepository = termCommentCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    [WithCleanCache(Keies = "TermComments")]
    public Task<string> HandleAsync(UpdateCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as TermComment;

        targetComment.Change(_dateTime, command.UserId, _serializer.Serialize(command.UserRoles), command.Comment);

        _termCommentCommandRepository.Change(targetComment);

        return Task.FromResult(targetComment.Id);
    }

    public Task AfterTransactionHandleAsync(UpdateCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}