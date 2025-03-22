using Domic.Core.Domain.Contracts.Interfaces;

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
    public IEnumerable<Entities.ArticleComment> FindAllEagerLoadingByArticleId(string articleId) 
        => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ownerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<Entities.ArticleComment>> FindAllEagerLoadingByOwnerIdAsync(string ownerId, 
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
}