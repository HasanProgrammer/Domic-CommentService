using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.TermComment.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = "Comment_TermComment_Exchange")]
public class TermCommentCreated : CreateDomainEvent<string>
{
    public required string TermId { get; init; }
    public required string Comment { get; init; }
}