using System.Linq.Expressions;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class ArticleCommentCommandRepository : IArticleCommentCommandRepository
{
    private readonly SQLContext _sqlContext;

    public ArticleCommentCommandRepository(SQLContext sqlContext) => _sqlContext = sqlContext;

    public async Task AddAsync(ArticleComment entity, CancellationToken cancellationToken)
        => await _sqlContext.ArticleComments.AddAsync(entity, cancellationToken);

    public void Change(ArticleComment entity) => _sqlContext.ArticleComments.Update(entity);

    public Task<bool> IsExistByIdAsync(string id, CancellationToken cancellationToken) 
        => _sqlContext.ArticleComments.AnyAsync(comment => comment.Id == id, cancellationToken);

    public async Task<ArticleComment> FindByIdAsync(object id, CancellationToken cancellationToken)
        => await _sqlContext.ArticleComments.FirstOrDefaultAsync(comment => comment.Id.Equals(id), cancellationToken);

    public IEnumerable<ArticleComment> FindAllEagerLoadingByArticleId(string articleId)
        => _sqlContext.ArticleComments.Where(comment => comment.ArticleId.Equals(articleId))
                                      .Include(comment => comment.Answers)
                                      .ToList();

    public async Task<IEnumerable<TViewModel>> FindAllEagerLoadingByProjectionAsync<TViewModel>(
        Expression<Func<ArticleComment, TViewModel>> projection, CancellationToken cancellationToken
    )
    {
        return await _sqlContext.ArticleComments.Include(comment => comment.Answers)
                                                .AsNoTracking()
                                                .Select(projection)
                                                .ToListAsync(cancellationToken);
    }
}