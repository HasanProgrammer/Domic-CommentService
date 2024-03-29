using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.TermComment.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = "Comment_TermComment_Exchange")]
public class ArticleActived : UpdateDomainEvent<string>;