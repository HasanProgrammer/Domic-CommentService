using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.ArticleCommentAnswer.Events;

[MessageBroker(Queue = Broker.Comment_User_Queue)]
public class UserInActived : UpdateDomainEvent<string>
{
}