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

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Commands.InActive;

public class InActiveCommandHandler : ICommandHandler<InActiveCommand, string>
{
    private readonly object _validationResult;

    private readonly IDotrisDateTime                        _dotrisDateTime;
    private readonly ISerializer                            _serializer;
    private readonly IJsonWebToken                          _jsonWebToken;
    private readonly IEventCommandRepository                _eventCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public InActiveCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository,
        IEventCommandRepository eventCommandRepository, IDotrisDateTime dotrisDateTime, IJsonWebToken jsonWebToken,
        ISerializer serializer
    )
    {
        _dotrisDateTime                        = dotrisDateTime;
        _jsonWebToken                          = jsonWebToken;
        _serializer                            = serializer;
        _eventCommandRepository                = eventCommandRepository;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(InActiveCommand command, CancellationToken cancellationToken)
    {
        var answer = _validationResult as ArticleCommentAnswer;
        
        answer.InActive(_dotrisDateTime);

        _articleCommentAnswerCommandRepository.Change(answer);

        #region OutBox

        var events = answer.GetEvents.ToEntityOfEvent(_dotrisDateTime, _serializer, Service.CommentService,
            Table.ArticleCommentAnswerTable, Action.Update, _jsonWebToken.GetUsername(command.Token)
        );

        foreach (var @event in events)
            await _eventCommandRepository.AddAsync(@event, cancellationToken);

        #endregion

        return answer.Id;
    }
}