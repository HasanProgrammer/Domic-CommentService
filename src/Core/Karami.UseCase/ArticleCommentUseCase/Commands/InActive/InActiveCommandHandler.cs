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

    private readonly IDotrisDateTime                  _dotrisDateTime;
    private readonly ISerializer                      _serializer;
    private readonly IJsonWebToken                    _jsonWebToken;
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;
    private readonly IEventCommandRepository          _eventCommandRepository;

    public InActiveCommandHandler(IArticleCommentCommandRepository articleCommentCommandRepository,
        IEventCommandRepository eventCommandRepository, IDotrisDateTime dotrisDateTime, ISerializer serializer,
        IJsonWebToken jsonWebToken
    )
    {
        _dotrisDateTime                  = dotrisDateTime;
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
        
        targetComment.InActive(_dotrisDateTime);

        #region OutBox

        var events = targetComment.GetEvents.ToEntityOfEvent(_dotrisDateTime, _serializer, Service.CommentService,
            Table.ArticleCommentTable, Action.Update, _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        _articleCommentCommandRepository.Change(targetComment);

        return targetComment.Id;
    }
}