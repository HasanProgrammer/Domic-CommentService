using System.Linq.Expressions;
using Domic.Core.Domain.Enumerations;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class ArticleCommentCommandRepository(SQLContext sqlContext) : IArticleCommentCommandRepository
{
    public async Task AddAsync(ArticleComment entity, CancellationToken cancellationToken)
        => await sqlContext.ArticleComments.AddAsync(entity, cancellationToken);

    public Task<bool> IsExistByIdAsync(string id, CancellationToken cancellationToken) 
        => sqlContext.ArticleComments.AnyAsync(comment => comment.Id == id, cancellationToken);

    public async Task<ArticleComment> FindByIdAsync(object id, CancellationToken cancellationToken)
        => await sqlContext.ArticleComments.AsNoTracking().FirstOrDefaultAsync(comment => comment.Id == id as string, cancellationToken);

    public async Task<ArticleComment> FindByIdEagerLoadingAsync(object id, CancellationToken cancellationToken)
        => await sqlContext.ArticleComments.AsNoTracking()
                                           .Include(comment => comment.Answers)
                                           .FirstOrDefaultAsync(comment => comment.Id == id as string, cancellationToken);

    public Task<List<ArticleComment>> FindAllEagerLoadingByArticleIdAsync(string articleId, 
        CancellationToken cancellationToken
    ) => sqlContext.ArticleComments.AsNoTracking()
                                   .Where(comment => comment.ArticleId == articleId)
                                   .Include(comment => comment.Answers)
                                   .ToListAsync(cancellationToken);

    public Task<List<ArticleComment>> FindAllByOwnerIdAsync(string ownerId, CancellationToken cancellationToken)
        => sqlContext.ArticleComments.AsNoTracking()
                                     .Where(comment => comment.CreatedBy == ownerId)
                                     .Include(comment => comment.Answers)
                                     .ToListAsync(cancellationToken);    
                                      
    public async Task<IEnumerable<TViewModel>> FindAllEagerLoadingByProjectionAsync<TViewModel>(
        Expression<Func<ArticleComment, TViewModel>> projection, CancellationToken cancellationToken
    )
    {
        return await sqlContext.ArticleComments.Include(comment => comment.Answers)
                                               .AsNoTracking()
                                               .Select(projection)
                                               .ToListAsync(cancellationToken);
    }

    public Task<List<ArticleComment>> FindAllEagerLoadingByOwnerIdAsync(string ownerId, 
        CancellationToken cancellationToken
    ) => sqlContext.ArticleComments.AsNoTracking()
                                   .Where(comment => comment.CreatedBy == ownerId)
                                   .Include(comment => comment.Answers)
                                   .ToListAsync(cancellationToken);

    public Task<List<TViewModel>> FindAllWithOrderingByProjectionAsync<TViewModel>(
        Expression<Func<ArticleComment, TViewModel>> projection, Order order, bool accending, 
        CancellationToken cancellationToken
    )
    {
        var query = sqlContext.ArticleComments.AsNoTracking();

        if (order == Order.Id)
            query = accending ? query.OrderBy(comment => comment.Id) : query.OrderByDescending(comment => comment.Id);
        else if (order == Order.Date)
            query = accending ? query.OrderBy(comment => comment.CreatedAt.EnglishDate) : query.OrderByDescending(comment => comment.CreatedAt.EnglishDate);

        return query.Select(projection).ToListAsync(cancellationToken);
    }

    public Task ChangeAsync(ArticleComment entity, CancellationToken cancellationToken)
    {
        sqlContext.ArticleComments.Update(entity);

        return Task.CompletedTask;
    }

    public Task ChangeRangeAsync(IEnumerable<ArticleComment> entities, CancellationToken cancellationToken)
    {
        sqlContext.ArticleComments.UpdateRange(entities);

        return Task.CompletedTask;
    }
}