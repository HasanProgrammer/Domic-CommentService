using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Article.Events;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Karami.UseCase.ArticleUseCase.Events;

public class ActiveArticleConsumerEventBusHandler : IConsumerEventBusHandler<ArticleDeleted>
{
    private readonly IDateTime                              _dateTime;
    private readonly IArticleCommentCommandRepository       _articleCommentCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public ActiveArticleConsumerEventBusHandler(IArticleCommentCommandRepository articleCommentCommandRepository,
        IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, IDateTime dateTime
    )
    {
        _dateTime                              = dateTime;
        _articleCommentCommandRepository       = articleCommentCommandRepository;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    [WithTransaction]
    [WithCleanCache(Keies = Cache.ArticleComments)]
    public void Handle(ArticleDeleted @event)
    {
        var comments = _articleCommentCommandRepository.FindAllEagerLoadingByArticleId(@event.Id);

        foreach (var comment in comments)
        {
            comment.Delete(_dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
            
            _articleCommentCommandRepository.Change(comment);
            
            foreach (var answer in comment.Answers)
            {
                answer.Delete(_dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
                
                _articleCommentAnswerCommandRepository.Change(answer);
            }
        }
    }
}