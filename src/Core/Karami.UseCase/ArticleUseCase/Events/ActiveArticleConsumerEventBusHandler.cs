using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.Article.Events;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Karami.UseCase.ArticleUseCase.Events;

public class DeleteArticleConsumerEventBusHandler : IConsumerEventBusHandler<ArticleActived>
{
    private readonly IDotrisDateTime                        _dotrisDateTime;
    private readonly IArticleCommentCommandRepository       _articleCommentCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public DeleteArticleConsumerEventBusHandler(IArticleCommentCommandRepository articleCommentCommandRepository,
        IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository,
        IDotrisDateTime dotrisDateTime
    )
    {
        _dotrisDateTime                        = dotrisDateTime;
        _articleCommentCommandRepository       = articleCommentCommandRepository;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    [WithTransaction]
    [WithCleanCache(Keies = Cache.ArticleComments)]
    public void Handle(ArticleActived @event)
    {
        var comments = _articleCommentCommandRepository.FindAllEagerLoadingByArticleId(@event.Id);

        foreach (var comment in comments)
        {
            comment.Active(_dotrisDateTime, false);
            
            _articleCommentCommandRepository.Change(comment);
            
            foreach (var answer in comment.Answers)
            {
                answer.Active(_dotrisDateTime, false);
                
                _articleCommentAnswerCommandRepository.Change(answer);
            }
        }
    }
}