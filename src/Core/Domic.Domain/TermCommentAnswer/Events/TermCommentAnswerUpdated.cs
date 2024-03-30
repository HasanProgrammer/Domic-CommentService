using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.TermCommentAnswer.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = "Comment_TermCommentAnswer_Exchange")]
public class TermCommentAnswerUpdated : UpdateDomainEvent<string>
{
    public required string Answer { get; init; }
}