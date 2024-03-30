using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.Term.Events;

[MessageBroker(Queue = "Comment_Term_Queue")]
public class TermInActived : UpdateDomainEvent<string>;