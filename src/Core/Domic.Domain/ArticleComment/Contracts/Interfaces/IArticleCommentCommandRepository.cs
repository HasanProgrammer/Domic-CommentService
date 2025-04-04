using System.Linq.Expressions;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.ArticleComment.Contracts.Interfaces;

public interface IArticleCommentCommandRepository : ICommandRepository<Entities.ArticleComment, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> IsExistByIdAsync(string id, CancellationToken cancellationToken);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="articleId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<List<Entities.ArticleComment>> FindAllEagerLoadingByArticleIdAsync(string articleId, 
        CancellationToken cancellationToken
    );
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ownerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<List<Entities.ArticleComment>> FindAllEagerLoadingByOwnerIdAsync(string ownerId, 
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    /// <param name="projection"></param>
    /// <param name="order"></param>
    /// <param name="accending"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public new Task<List<TViewModel>> FindAllWithOrderingByProjectionAsync<TViewModel>(
        Expression<Func<Entities.ArticleComment, TViewModel>> projection, Order order, bool accending, 
        CancellationToken cancellationToken
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task ChangeRangeAsync(IEnumerable<Entities.ArticleComment> entities, CancellationToken cancellationToken);
}