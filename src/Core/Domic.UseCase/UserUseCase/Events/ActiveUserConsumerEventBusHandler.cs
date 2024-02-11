using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class ActiveUserConsumerEventBusHandler : IConsumerEventBusHandler<UserActived>
{
    private readonly IDateTime                              _dateTime;
    private readonly IArticleCommentCommandRepository       _articleCommentCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public ActiveUserConsumerEventBusHandler(IArticleCommentCommandRepository articleCommentCommandRepository,
        IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, IDateTime dateTime
    )
    {
        _dateTime                              = dateTime;
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
            comment.Active(_dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
            
            _articleCommentCommandRepository.Change(comment);

            foreach (var answer in comment.Answers)
            {
                answer.Active(_dateTime, @event.UpdatedBy, @event.UpdatedRole, false);
                
                _articleCommentAnswerCommandRepository.Change(answer);
            }
        }
        
        //Active all user answers
        
        var answers = _articleCommentAnswerCommandRepository.FindAllByOwnerIdAsync(@event.Id, default)
                                                            .GetAwaiter()
                                                            .GetResult();

        foreach (var answer in answers)
        {
            answer.Active(_dateTime, @event.UpdatedBy, @event.UpdatedRole);

            _articleCommentAnswerCommandRepository.Change(answer);
        }
    }
}