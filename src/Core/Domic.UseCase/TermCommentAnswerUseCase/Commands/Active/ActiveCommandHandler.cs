#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Active;

public class ActiveCommandHandler : ICommandHandler<ActiveCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime _dateTime;
    private readonly ISerializer _serializer;
    private readonly ITermCommentAnswerCommandRepository _termCommentAnswerCommandRepository;

    public ActiveCommandHandler(ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository,
        IDateTime dateTime, ISerializer serializer
    )
    {
        _dateTime = dateTime;
        _serializer = serializer;
        _termCommentAnswerCommandRepository = termCommentAnswerCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(ActiveCommand command, CancellationToken cancellationToken)
    {
        var answer = _validationResult as TermCommentAnswer;

        answer.Active(_dateTime, command.UserId, _serializer.Serialize(command.UserRoles));

        _termCommentAnswerCommandRepository.Change(answer);

        return Task.FromResult(answer.Id);
    }

    public Task AfterTransactionHandleAsync(ActiveCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}