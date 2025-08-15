using System.Linq.Expressions;
using Domic.Domain.TermComment.Entities;
using Domic.Domain.TermComment.Contracts.Interfaces;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class TermCommentCommandRepository(SQLContext sqlContext) : ITermCommentCommandRepository
{
    public Task AddAsync(TermComment entity, CancellationToken cancellationToken)
    {
        sqlContext.TermComments.Add(entity);

        return Task.CompletedTask;
    }

    public Task ChangeAsync(TermComment entity, CancellationToken cancellationToken)
    {
        sqlContext.TermComments.Update(entity);

        return Task.CompletedTask;
    }

    public Task<TermComment> FindByIdAsync(object id, CancellationToken cancellationToken)
        => sqlContext.TermComments.FirstOrDefaultAsync(comment => comment.Id == id as string, cancellationToken);

    public Task<TermComment> FindByIdEagerLoadingAsync(object id, CancellationToken cancellationToken) 
        => sqlContext.TermComments.AsNoTracking()
                                  .Where(comment => comment.Id == id as string)
                                  .Include(comment => comment.Answers)
                                  .FirstOrDefaultAsync(cancellationToken);

    public Task<List<TermComment>> FindAllEagerLoadingByTermIdAsync(string termId, 
        CancellationToken cancellationToken
    ) => sqlContext.TermComments.AsNoTracking()
                                .Where(comment => comment.TermId == termId)
                                .Include(comment => comment.Answers)
                                .ToListAsync(cancellationToken);

    public async Task<List<TViewModel>> FindAllWithOrderingByProjectionAsync<TViewModel>(
        Expression<Func<TermComment, TViewModel>> projection, CancellationToken cancellationToken
    ) => await sqlContext.TermComments.Include(comment => comment.Answers)
                                      .AsNoTracking()
                                      .OrderByDescending(comment => comment.CreatedAt.EnglishDate)
                                      .Select(projection)
                                      .ToListAsync(cancellationToken);

    public Task<bool> IsExistByIdAsync(string id, CancellationToken cancellationToken)
        => sqlContext.TermComments.AnyAsync(comment => comment.Id == id, cancellationToken);
}