using Domic.Persistence.Configs.C;
using Domic.Core.Domain.Entities;
using Domic.Core.Persistence.Configs;
using Domic.Domain.ArticleComment.Entities;
using Domic.Domain.ArticleCommentAnswer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domic.Persistence.Contexts.C;

/*Setting*/
public partial class SQLContext : DbContext
{
    public SQLContext(DbContextOptions<SQLContext> options) : base(options)
    {
        
    }
}

/*Entity*/
public partial class SQLContext
{
    public DbSet<Event> Events                               { get; set; }
    public DbSet<ArticleComment> ArticleComments             { get; set; }
    public DbSet<ArticleCommentAnswer> ArticleCommentAnswers { get; set; }
}

/*Config*/
public partial class SQLContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new EventConfig());
        builder.ApplyConfiguration(new ArticleCommentConfig());
        builder.ApplyConfiguration(new ArticleCommentAnswerConfig());
    }
}