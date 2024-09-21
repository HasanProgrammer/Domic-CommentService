using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.TermCommentAnswer.Events;

[EventConfig(ExchangeType = Exchange.FanOut, Exchange = "Comment_TermCommentAnswer_Exchange")]
public class TermCommentAnswerActived : UpdateDomainEvent<string>;