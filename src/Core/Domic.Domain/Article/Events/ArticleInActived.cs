using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.Article.Events;

[MessageBroker(Queue = Broker.Comment_Article_Queue)]
public class ArticleInActived : UpdateDomainEvent<string>
{
    
}