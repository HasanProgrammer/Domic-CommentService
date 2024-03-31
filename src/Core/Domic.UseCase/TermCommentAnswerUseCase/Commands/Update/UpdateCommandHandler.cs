#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Update;

public class UpdateCommandHandler : ICommandHandler<UpdateCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime _dateTime;
    private readonly ISerializer _serializer;
    private readonly ITermCommentAnswerCommandRepository _repository;

    public UpdateCommandHandler(ITermCommentAnswerCommandRepository repository, IDateTime dateTime,
        ISerializer serializer
    )
    {
        _dateTime = dateTime;
        _serializer = serializer;
        _repository = repository;
    }

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(UpdateCommand command, CancellationToken cancellationToken)
    {
        var answer = _validationResult as TermCommentAnswer;

        answer.Change(_dateTime, command.UserId, _serializer.Serialize(command.UserRoles), command.Answer);

        _repository.Change(answer);

        return Task.FromResult(answer.Id);
    }
}