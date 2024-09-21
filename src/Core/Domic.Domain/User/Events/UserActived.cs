using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.ArticleCommentAnswer.Events;

[EventConfig(Queue = Broker.Comment_User_Queue)]
public class UserActived : UpdateDomainEvent<string>
{
}