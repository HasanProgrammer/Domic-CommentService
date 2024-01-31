using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Core.Domain.Enumerations;

namespace Karami.Domain.ArticleComment.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = Broker.Comment_ArticleComment_Exchange)]
public class ArticleCommentUpdated : UpdateDomainEvent<string>
{
    public string Comment { get; init; }
}