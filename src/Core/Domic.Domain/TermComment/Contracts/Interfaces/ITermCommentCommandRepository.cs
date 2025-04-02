using System.Linq.Expressions;
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

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    /// <param name="projection"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<List<TViewModel>> FindAllWithOrderingByProjectionAsync<TViewModel>(
        Expression<Func<Entities.TermComment, TViewModel>> projection, CancellationToken cancellationToken
    ) => throw new NotImplementedException();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> IsExistByIdAsync(string id, CancellationToken cancellationToken);
}