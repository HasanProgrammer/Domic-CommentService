﻿using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Article.Events;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.ArticleUseCase.Events;

public class ActiveArticleConsumerEventBusHandler : IConsumerEventBusHandler<ArticleActived>
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

    public void BeforeHandle(ArticleActived @event){}

    [TransactionConfig(Type = TransactionType.Command)]
    [WithCleanCache(Keies = Cache.ArticleComments)]
    public void Handle(ArticleActived @event)
    {
        var comments = _articleCommentCommandRepository.FindAllEagerLoadingByArticleId(@event.Id);

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
    }

    public void AfterHandle(ArticleActived @event){}
}