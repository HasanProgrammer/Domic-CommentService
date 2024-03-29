using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.ArticleCommentAnswer.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = Broker.Comment_ArticleCommentAnswer_Exchange)]
public class ArticleCommentAnswerActived : UpdateDomainEvent<string>;