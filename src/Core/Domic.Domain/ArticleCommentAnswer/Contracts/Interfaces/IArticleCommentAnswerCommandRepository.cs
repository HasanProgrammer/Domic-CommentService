using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

public interface IArticleCommentAnswerCommandRepository : ICommandRepository<Entities.ArticleCommentAnswer, string>
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
    /// <param name="ownerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<Entities.ArticleCommentAnswer>> FindAllByOwnerIdAsync(string ownerId, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task ChangeRangeAsync(IEnumerable<Entities.ArticleCommentAnswer> entities, CancellationToken cancellationToken);
}