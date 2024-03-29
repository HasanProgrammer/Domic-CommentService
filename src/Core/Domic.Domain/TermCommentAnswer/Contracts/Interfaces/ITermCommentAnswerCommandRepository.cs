using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.TermCommentAnswer.Contracts.Interfaces;

public class ITermCommentAnswerCommandRepository : ICommandRepository<Entities.TermCommentAnswer, string>;