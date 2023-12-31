﻿using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Core.Domain.Enumerations;

namespace Karami.Domain.ArticleCommentAnswer.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = Broker.Comment_ArticleCommentAnswer_Exchange)]
public class ArticleCommentAnswerCreated : CreateDomainEvent
{
    public string Id        { get; init; }
    public string OwnerId   { get; init; }
    public string CommentId { get; init; }
    public string Answer    { get; init; }
}