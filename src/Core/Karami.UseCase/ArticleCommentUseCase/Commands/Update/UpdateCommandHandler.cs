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

    private readonly IDotrisDateTime                  _dotrisDateTime;
    private readonly ISerializer                      _serializer;
    private readonly IJsonWebToken                    _jsonWebToken;
    private readonly IEventCommandRepository          _eventCommandRepository;
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;

    public UpdateCommandHandler(IArticleCommentCommandRepository articleCommentCommandRepository,
        IEventCommandRepository eventCommandRepository, IDotrisDateTime dotrisDateTime, ISerializer serializer
    )
    {
        _dotrisDateTime                  = dotrisDateTime;
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
        
        targetComment.Change(_dotrisDateTime, command.Comment);

        #region OutBox

        var events = targetComment.GetEvents.ToEntityOfEvent(_dotrisDateTime, _serializer,
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