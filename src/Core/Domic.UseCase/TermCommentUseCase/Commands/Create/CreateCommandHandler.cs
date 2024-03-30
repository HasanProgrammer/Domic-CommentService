using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Domain.TermComment.Entities;

namespace Domic.UseCase.TermCommentUseCase.Commands.Create;

public class CreateCommandHandler : ICommandHandler<CreateCommand, string>
{
    private readonly IDateTime                     _dateTime;
    private readonly ISerializer                   _serializer;
    private readonly ITermCommentCommandRepository _termCommentCommandRepository;
    private readonly IGlobalUniqueIdGenerator      _globalUniqueIdGenerator;

    public CreateCommandHandler(ITermCommentCommandRepository termCommentCommandRepository, 
        IDateTime dateTime, ISerializer serializer, IGlobalUniqueIdGenerator globalUniqueIdGenerator
    )
    {
        _dateTime                     = dateTime;
        _serializer                   = serializer;
        _termCommentCommandRepository = termCommentCommandRepository;
        _globalUniqueIdGenerator      = globalUniqueIdGenerator;
    }

    [WithTransaction]
    public Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var newComment = new TermComment(
            _dateTime, _globalUniqueIdGenerator, command.UserId, command.TermId, 
            _serializer.Serialize(command.UserRoles), command.Comment
        );

        _termCommentCommandRepository.Add(newComment);

        return Task.FromResult(newComment.Id);
    }
}