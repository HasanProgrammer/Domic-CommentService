using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;

namespace Domic.UseCase.TermCommentAnswerUseCase.Commands.Create;

public class CreateCommandHandler : ICommandHandler<CreateCommand, string>
{
    private readonly IDateTime _dateTime;
    private readonly ISerializer _serializer;
    private readonly IGlobalUniqueIdGenerator _globalUniqueIdGenerator;
    private readonly ITermCommentAnswerCommandRepository _termCommentAnswerCommandRepository;

    public CreateCommandHandler(ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository,
        IDateTime dateTime, ISerializer serializer, IGlobalUniqueIdGenerator globalUniqueIdGenerator
    )
    {
        _dateTime = dateTime;
        _serializer = serializer;
        _globalUniqueIdGenerator = globalUniqueIdGenerator;
        _termCommentAnswerCommandRepository = termCommentAnswerCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var newAnswer = new TermCommentAnswer(
            _dateTime, _globalUniqueIdGenerator, command.UserId, _serializer.Serialize(command.UserRoles),
            command.CommentId, command.Answer
        );

        _termCommentAnswerCommandRepository.Add(newAnswer);

        return Task.FromResult(newAnswer.Id);
    }

    public Task AfterTransactionHandleAsync(CreateCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}