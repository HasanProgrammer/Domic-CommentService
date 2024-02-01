using Karami.Common.ClassConsts;
using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.Domain.Extensions;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Entities;

using Action = Karami.Core.Common.ClassConsts.Action;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Commands.Create;

public class CreateCommandHandler : ICommandHandler<CreateCommand, string>
{
    private readonly IDateTime                              _dateTime;
    private readonly ISerializer                            _serializer;
    private readonly IJsonWebToken                          _jsonWebToken;
    private readonly IGlobalUniqueIdGenerator               _globalUniqueIdGenerator;
    private readonly IEventCommandRepository                _eventCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public CreateCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, 
        IEventCommandRepository eventCommandRepository, IDateTime dateTime, ISerializer serializer, 
        IJsonWebToken jsonWebToken, IGlobalUniqueIdGenerator globalUniqueIdGenerator
    )
    {
        _dateTime                              = dateTime;
        _serializer                            = serializer;
        _jsonWebToken                          = jsonWebToken;
        _globalUniqueIdGenerator               = globalUniqueIdGenerator;
        _eventCommandRepository                = eventCommandRepository;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var uniqueIdentity = _globalUniqueIdGenerator.GetRandom();
        var createdBy      = _jsonWebToken.GetIdentityUserId(command.Token);
        var createdRole    = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        var newAnswer = new ArticleCommentAnswer(
            _dateTime, uniqueIdentity, createdBy, createdRole, command.CommentId, command.Answer
        );

        #region OutBox

        var events = newAnswer.GetEvents.ToEntityOfEvent( _dateTime, _serializer, Service.CommentService, 
            Table.ArticleCommentAnswerTable, Action.Create, _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        await _articleCommentAnswerCommandRepository.AddAsync(newAnswer, cancellationToken);

        return newAnswer.Id;
    }
}