﻿using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Core.Domain.Enumerations;

namespace Karami.Domain.ArticleComment.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = Broker.Comment_ArticleComment_Exchange)]
public class ArticleCommentCreated : CreateDomainEvent
{
    public string Id        { get; init; }
    public string OwnerId   { get; init; }
    public string ArticleId { get; init; }
    public string Comment   { get; init; }
}