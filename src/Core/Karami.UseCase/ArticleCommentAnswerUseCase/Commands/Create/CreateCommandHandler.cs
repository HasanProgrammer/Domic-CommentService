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
    private readonly IDotrisDateTime                        _dotrisDateTime;
    private readonly ISerializer                            _serializer;
    private readonly IJsonWebToken                          _jsonWebToken;
    private readonly IEventCommandRepository                _eventCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public CreateCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, 
        IEventCommandRepository eventCommandRepository, 
        IDotrisDateTime dotrisDateTime, 
        ISerializer serializer, 
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
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var newAnswer = new ArticleCommentAnswer(
            _dotrisDateTime, Guid.NewGuid().ToString(), command.OwnerId, command.CommentId, command.Answer
        );

        #region OutBox

        var events = newAnswer.GetEvents.ToEntityOfEvent( _dotrisDateTime, _serializer,
            Service.CommentService, Table.ArticleCommentAnswerTable, Action.Create,
            _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        await _articleCommentAnswerCommandRepository.AddAsync(newAnswer, cancellationToken);

        return newAnswer.Id;
    }
}