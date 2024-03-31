using System.Linq.Expressions;
using Domic.Domain.TermCommentAnswer.Contracts.Interfaces;
using Domic.Domain.TermCommentAnswer.Entities;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class TermCommentAnswerAnswerCommandRepository(SQLContext sqlContext) : ITermCommentAnswerCommandRepository
{
    public void Add(TermCommentAnswer entity) => sqlContext.TermCommentAnswers.Add(entity);

    public void Change(TermCommentAnswer entity) => sqlContext.TermCommentAnswers.Update(entity);

    public Task<TermCommentAnswer> FindByIdAsync(object id, CancellationToken cancellationToken)
        => sqlContext.TermCommentAnswers.FirstOrDefaultAsync(comment => comment.Id.Equals(id), cancellationToken);

    public Task<List<TermCommentAnswer>> FindAllEagerLoadingByTermIdAsync(string commentId, 
        CancellationToken cancellationToken
    ) => sqlContext.TermCommentAnswers.Where(comment => comment.CommentId.Equals(commentId))
                                      .Include(comment => comment.Comment)
                                      .ToListAsync(cancellationToken);

    public async Task<IEnumerable<TViewModel>> FindAllEagerLoadingByProjectionAsync<TViewModel>(
        Expression<Func<TermCommentAnswer, TViewModel>> projection, CancellationToken cancellationToken
    ) => await sqlContext.TermCommentAnswers.Include(comment => comment.Comment)
                                            .AsNoTracking()
                                            .Select(projection)
                                            .ToListAsync(cancellationToken);
}