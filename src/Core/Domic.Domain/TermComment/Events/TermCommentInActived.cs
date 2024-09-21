using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.TermComment.Events;

[EventConfig(ExchangeType = Exchange.FanOut, Exchange = "Comment_TermComment_Exchange")]
public class TermCommentInActived : UpdateDomainEvent<string>;