using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

public interface IArticleCommentAnswerCommandRepository : ICommandRepository<Entities.ArticleCommentAnswer, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ownerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<Entities.ArticleCommentAnswer>> FindAllByOwnerIdAsync(string ownerId,
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ownerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<Entities.ArticleCommentAnswer>> FindAllEagerLoadingByOwnerIdAsync(string ownerId,
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
}