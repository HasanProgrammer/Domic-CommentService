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

namespace Karami.UseCase.ArticleCommentUseCase.Commands.Update;

public class UpdateCommandHandler : ICommandHandler<UpdateCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                        _dateTime;
    private readonly ISerializer                      _serializer;
    private readonly IJsonWebToken                    _jsonWebToken;
    private readonly IEventCommandRepository          _eventCommandRepository;
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;

    public UpdateCommandHandler(IArticleCommentCommandRepository articleCommentCommandRepository,
        IEventCommandRepository eventCommandRepository, IDateTime dateTime, ISerializer serializer
    )
    {
        _dateTime                        = dateTime;
        _serializer                      = serializer;
        _eventCommandRepository          = eventCommandRepository;
        _articleCommentCommandRepository = articleCommentCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    [WithCleanCache(Keies = Cache.ArticleComments)]
    public async Task<string> HandleAsync(UpdateCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as ArticleComment;
        var updatedBy     = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole   = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        targetComment.Change(_dateTime, updatedBy, updatedRole, command.Comment);

        #region OutBox

        var events = targetComment.GetEvents.ToEntityOfEvent(_dateTime, _serializer,
            Service.AggregateArticleService, Table.ArticleCommentAnswerTable, Action.Update, 
            _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        await _articleCommentCommandRepository.ChangeAsync(targetComment, cancellationToken);

        return targetComment.Id;
    }
}