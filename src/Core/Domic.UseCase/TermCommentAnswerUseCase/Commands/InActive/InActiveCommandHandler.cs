﻿#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.InActive;

public class InActiveCommandHandler : ICommandHandler<InActiveCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime _dateTime;
    private readonly ISerializer _serializer;
    private readonly ITermCommentAnswerCommandRepository _repository;

    public InActiveCommandHandler(ITermCommentAnswerCommandRepository repository, IDateTime dateTime,
        ISerializer serializer
    )
    {
        _dateTime = dateTime;
        _serializer = serializer;
        _repository = repository;
    }

    public Task BeforeHandleAsync(InActiveCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(InActiveCommand command, CancellationToken cancellationToken)
    {
        var answer = _validationResult as TermCommentAnswer;

        answer.InActive(_dateTime, command.UserId, _serializer.Serialize(command.UserRoles));

        _repository.Change(answer);

        return Task.FromResult(answer.Id);
    }

    public Task AfterTransactionHandleAsync(InActiveCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}