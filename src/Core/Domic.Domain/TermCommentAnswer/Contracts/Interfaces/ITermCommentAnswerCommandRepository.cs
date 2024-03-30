using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.TermCommentAnswer.Contracts.Interfaces;

public interface ITermCommentAnswerCommandRepository : ICommandRepository<Entities.TermCommentAnswer, string>;