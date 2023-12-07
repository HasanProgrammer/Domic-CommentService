using Karami.Core.Common.ClassConsts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Karami.Domain.ArticleCommentAnswer.Events;

namespace Karami.UseCase.UserUseCase.Events;

public class ActiveUserConsumerEventBusHandler : IConsumerEventBusHandler<UserActived>
{
    private readonly IDotrisDateTime                        _dotrisDateTime;
    private readonly IArticleCommentCommandRepository       _articleCommentCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public ActiveUserConsumerEventBusHandler(IArticleCommentCommandRepository articleCommentCommandRepository,
        IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, IDotrisDateTime dotrisDateTime
    )
    {
        _dotrisDateTime                        = dotrisDateTime;
        _articleCommentCommandRepository       = articleCommentCommandRepository;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    [WithTransaction]
    [WithCleanCache(Keies = Cache.ArticleComments)]
    public void Handle(UserActived @event)
    {
        //Active all user comments by answers
        
        var comments =
            _articleCommentCommandRepository.FindAllEagerLoadingByOwnerIdAsync(@event.Id, default)
                                            .GetAwaiter()
                                            .GetResult();

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
        
        //Active all user answers
        
        var answers = _articleCommentAnswerCommandRepository.FindAllByOwnerIdAsync(@event.Id, default)
                                                            .GetAwaiter()
                                                            .GetResult();

        foreach (var answer in answers)
        {
            answer.Active(_dotrisDateTime);

            _articleCommentAnswerCommandRepository.Change(answer);
        }
    }
}