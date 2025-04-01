using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class ArticleCommentAnswerCommandRepository(SQLContext sqlContext) : IArticleCommentAnswerCommandRepository
{
    public Task<bool> IsExistByIdAsync(string id, CancellationToken cancellationToken) 
        => sqlContext.ArticleCommentAnswers.AnyAsync(answer => answer.Id == id, cancellationToken);

    public Task<ArticleCommentAnswer> FindByIdAsync(object id, CancellationToken cancellationToken)
        => sqlContext.ArticleCommentAnswers.FirstOrDefaultAsync(answer => answer.Id == id as string, cancellationToken);

    public Task<List<ArticleCommentAnswer>> FindAllByOwnerIdAsync(string ownerId, CancellationToken cancellationToken)
        => sqlContext.ArticleCommentAnswers.AsNoTracking()
                                           .Where(answer => answer.CreatedBy == ownerId)
                                           .ToListAsync(cancellationToken);

    public Task AddAsync(ArticleCommentAnswer entity, CancellationToken cancellationToken)
    {
        sqlContext.ArticleCommentAnswers.Add(entity);

        return Task.CompletedTask;
    }

    public Task ChangeAsync(ArticleCommentAnswer entity, CancellationToken cancellationToken)
    {
        sqlContext.ArticleCommentAnswers.Update(entity);

        return Task.CompletedTask;
    }

    public Task ChangeRangeAsync(IEnumerable<ArticleCommentAnswer> entities, CancellationToken cancellationToken)
    {
        sqlContext.ArticleCommentAnswers.UpdateRange(entities);

        return Task.CompletedTask;
    }
}