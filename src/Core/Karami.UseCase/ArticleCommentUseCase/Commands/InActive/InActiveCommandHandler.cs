#pragma warning disable CS0649

using Karami.Common.ClassConsts;
using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.Domain.Extensions;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Entities;

using Action = Karami.Core.Common.ClassConsts.Action;

namespace Karami.UseCase.ArticleCommentUseCase.Commands.InActive;

public class InActiveCommandHandler : ICommandHandler<InActiveCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                        _dateTime;
    private readonly ISerializer                      _serializer;
    private readonly IJsonWebToken                    _jsonWebToken;
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;
    private readonly IEventCommandRepository          _eventCommandRepository;

    public InActiveCommandHandler(IArticleCommentCommandRepository articleCommentCommandRepository,
        IEventCommandRepository eventCommandRepository, IDateTime dateTime, ISerializer serializer, 
        IJsonWebToken jsonWebToken
    )
    {
        _dateTime                        = dateTime;
        _serializer                      = serializer;
        _jsonWebToken                    = jsonWebToken;
        _eventCommandRepository          = eventCommandRepository;
        _articleCommentCommandRepository = articleCommentCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(InActiveCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as ArticleComment;
        var updatedBy     = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole   = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        targetComment.InActive(_dateTime, updatedBy, updatedRole);

        #region OutBox

        var events = targetComment.GetEvents.ToEntityOfEvent(_dateTime, _serializer, Service.CommentService,
            Table.ArticleCommentTable, Action.Update, _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        _articleCommentCommandRepository.Change(targetComment);

        return targetComment.Id;
    }
}