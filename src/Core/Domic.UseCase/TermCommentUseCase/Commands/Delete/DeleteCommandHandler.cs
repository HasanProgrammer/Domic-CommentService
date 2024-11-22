#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermComment.Entities;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Commands.Delete;

public class DeleteCommandHandler : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                           _dateTime;
    private readonly ISerializer                         _serializer;
    private readonly ITermCommentCommandRepository       _termCommentCommandRepository;
    private readonly ITermCommentAnswerCommandRepository _termCommentAnswerCommandRepository;

    public DeleteCommandHandler(
        ITermCommentCommandRepository termCommentCommandRepository,
        ITermCommentAnswerCommandRepository termCommentAnswerCommandRepository, IDateTime dateTime, 
        ISerializer serializer
    )
    {
        _dateTime                           = dateTime;
        _serializer                         = serializer;
        _termCommentCommandRepository       = termCommentCommandRepository;
        _termCommentAnswerCommandRepository = termCommentAnswerCommandRepository;
    }

    public Task BeforeHandleAsync(DeleteCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as TermComment;
        
        targetComment.Delete(_dateTime, command.UserId, _serializer.Serialize(command.UserRoles));

        _termCommentCommandRepository.Change(targetComment);

        foreach (var answer in targetComment.Answers)
        {
            answer.Delete(_dateTime, command.UserId, _serializer.Serialize(command.UserRoles), false);
            
            _termCommentAnswerCommandRepository.Change(answer);
        }

        return Task.FromResult(targetComment.Id);
    }

    public Task AfterHandleAsync(DeleteCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}