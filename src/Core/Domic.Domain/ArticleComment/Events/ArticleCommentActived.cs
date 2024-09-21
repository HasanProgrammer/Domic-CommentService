using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.ArticleComment.Events;

[EventConfig(ExchangeType = Exchange.FanOut, Exchange = Broker.Comment_ArticleComment_Exchange)]
public class ArticleCommentActived : UpdateDomainEvent<string>;