using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.TermCommentAnswer.Contracts.Interfaces;

public interface ITermCommentAnswerCommandRepository : ICommandRepository<Entities.TermCommentAnswer, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task ChangeRangeAsync(IEnumerable<Entities.TermCommentAnswer> entities, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> IsExistByIdAsync(string id, CancellationToken cancellationToken);
}