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

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Commands.Delete;

public class DeleteCommandHandler : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;

    private readonly IDotrisDateTime                        _dotrisDateTime;
    private readonly ISerializer                            _serializer;
    private readonly IJsonWebToken                          _jsonWebToken;
    private readonly IEventCommandRepository                _eventCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public DeleteCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, 
        IEventCommandRepository eventCommandRepository, IDotrisDateTime dotrisDateTime, ISerializer serializer,
        IJsonWebToken jsonWebToken
    )
    {
        _dotrisDateTime                        = dotrisDateTime;
        _serializer                            = serializer;
        _jsonWebToken                          = jsonWebToken;
        _eventCommandRepository                = eventCommandRepository;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetAnswer = _validationResult as ArticleCommentAnswer;
        
        targetAnswer.Delete(_dotrisDateTime);
        
        _articleCommentAnswerCommandRepository.Change(targetAnswer);

        #region OutBox

        var events = targetAnswer.GetEvents.ToEntityOfEvent( _dotrisDateTime, _serializer,
            Service.CommentService, Table.ArticleCommentAnswerTable, Action.Create,
            _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        return targetAnswer.Id;
    }
}