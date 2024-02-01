#pragma warning disable CS0649

using Karami.Common.ClassConsts;
using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.Domain.Extensions;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Entities;

using Action = Karami.Core.Common.ClassConsts.Action;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Commands.Active;

public class ActiveCommandHandler : ICommandHandler<ActiveCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                              _dateTime;
    private readonly ISerializer                            _serializer;
    private readonly IJsonWebToken                          _jsonWebToken;
    private readonly IEventCommandRepository                _eventCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public ActiveCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository,
        IEventCommandRepository eventCommandRepository, IDateTime dateTime, IJsonWebToken jsonWebToken,
        ISerializer serializer
    )
    {
        _dateTime                              = dateTime;
        _jsonWebToken                          = jsonWebToken;
        _serializer                            = serializer;
        _eventCommandRepository                = eventCommandRepository;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(ActiveCommand command, CancellationToken cancellationToken)
    {
        var answer      = _validationResult as ArticleCommentAnswer;
        var updatedBy   = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        answer.Active(_dateTime, updatedBy, updatedRole);

        _articleCommentAnswerCommandRepository.Change(answer);

        #region OutBox

        var events = answer.GetEvents.ToEntityOfEvent(_dateTime, _serializer, Service.CommentService,
            Table.ArticleCommentAnswerTable, Action.Update, _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        return answer.Id;
    }
}