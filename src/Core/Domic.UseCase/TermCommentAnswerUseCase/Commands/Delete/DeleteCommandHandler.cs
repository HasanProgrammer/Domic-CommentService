#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Delete;

public class DeleteCommandHandler : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime _dateTime;
    private readonly ISerializer _serializer;
    private readonly ITermCommentAnswerCommandRepository _repository;

    public DeleteCommandHandler(ITermCommentAnswerCommandRepository repository,
        IDateTime dateTime, ISerializer serializer
    )
    {
        _dateTime = dateTime;
        _serializer = serializer;
        _repository = repository;
    }

    public Task BeforeHandleAsync(DeleteCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetAnswer = _validationResult as TermCommentAnswer;

        targetAnswer.Delete(_dateTime, command.UserId, _serializer.Serialize(command.UserRoles));

        _repository.Change(targetAnswer);

        return Task.FromResult(targetAnswer.Id);
    }

    public Task AfterTransactionHandleAsync(DeleteCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}