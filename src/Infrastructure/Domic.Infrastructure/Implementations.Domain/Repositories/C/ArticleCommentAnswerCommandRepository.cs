using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class ArticleCommentAnswerCommandRepository : IArticleCommentAnswerCommandRepository
{
    private readonly SQLContext _sqlContext;

    public ArticleCommentAnswerCommandRepository(SQLContext sqlContext) => _sqlContext = sqlContext;

    public async Task<ArticleCommentAnswer> FindByIdAsync(object id, CancellationToken cancellationToken)
        => await _sqlContext.ArticleCommentAnswers.FirstOrDefaultAsync(answer => answer.Id.Equals(id), cancellationToken);

    public async Task AddAsync(ArticleCommentAnswer entity, CancellationToken cancellationToken)
        => await _sqlContext.ArticleCommentAnswers.AddAsync(entity, cancellationToken);

    public void Change(ArticleCommentAnswer entity) => _sqlContext.ArticleCommentAnswers.Update(entity);
}