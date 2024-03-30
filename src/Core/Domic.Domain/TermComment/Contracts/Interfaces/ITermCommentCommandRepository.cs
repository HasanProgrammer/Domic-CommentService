using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.TermComment.Contracts.Interfaces;

public interface ITermCommentCommandRepository : ICommandRepository<Entities.TermComment, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="termId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<List<Entities.TermComment>> FindAllEagerLoadingByTermIdAsync(string termId,
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
}