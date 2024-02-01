using Karami.Common.ClassConsts;
using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.Domain.Extensions;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Entities;

using Action = Karami.Core.Common.ClassConsts.Action;

namespace Karami.UseCase.ArticleCommentUseCase.Commands.Create;

public class CreateCommandHandler : ICommandHandler<CreateCommand, string>
{
    private readonly IDateTime                        _dateTime;
    private readonly ISerializer                      _serializer;
    private readonly IJsonWebToken                    _jsonWebToken;
    private readonly IEventCommandRepository          _eventCommandRepository;
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;
    private readonly IGlobalUniqueIdGenerator         _globalUniqueIdGenerator;

    public CreateCommandHandler(IArticleCommentCommandRepository articleCommentCommandRepository, 
        IEventCommandRepository eventCommandRepository, IDateTime dateTime, ISerializer serializer, 
        IJsonWebToken jsonWebToken, IGlobalUniqueIdGenerator globalUniqueIdGenerator
    )
    {
        _dateTime                        = dateTime;
        _serializer                      = serializer;
        _jsonWebToken                    = jsonWebToken;
        _eventCommandRepository          = eventCommandRepository;
        _articleCommentCommandRepository = articleCommentCommandRepository;
        _globalUniqueIdGenerator         = globalUniqueIdGenerator;
    }

    [WithTransaction]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var uniqueIdentity = _globalUniqueIdGenerator.GetRandom();
        var createdBy      = _jsonWebToken.GetIdentityUserId(command.Token);
        var createdRole    = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        var newComment = new ArticleComment(
            _dateTime, uniqueIdentity, createdBy, command.ArticleId, createdRole, command.Comment
        );

        #region OutBox

        var events = newComment.GetEvents.ToEntityOfEvent(_dateTime, _serializer, 
            Service.CommentService, Table.ArticleCommentTable, Action.Create, _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        await _articleCommentCommandRepository.AddAsync(newComment, cancellationToken);

        return newComment.Id;
    }
}