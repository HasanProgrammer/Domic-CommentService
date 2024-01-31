using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Constants;
using Karami.Core.Domain.Contracts.Abstracts;

namespace Karami.Domain.ArticleCommentAnswer.Events;

[MessageBroker(Queue = Broker.Comment_User_Queue)]
public class UserInActived : UpdateDomainEvent<string>
{
}