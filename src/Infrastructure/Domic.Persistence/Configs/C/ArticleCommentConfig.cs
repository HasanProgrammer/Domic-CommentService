using Domic.Core.Persistence.Configs;
using Domic.Domain.ArticleComment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domic.Persistence.Configs.C;

public class ArticleCommentConfig : BaseEntityConfig<ArticleComment, string>
{
    public override void Configure(EntityTypeBuilder<ArticleComment> builder)
    {
        base.Configure(builder);
        
        /*-----------------------------------------------------------*/

        //Configs

        builder.ToTable("ArticleComments");
        
        builder.Property(comment => comment.ArticleId).IsRequired();
        
        builder.OwnsOne(comment => comment.Comment)
               .Property(comment => comment.Value)
               .IsRequired()
               .HasMaxLength(800)
               .HasColumnName("Comment");

        /*-----------------------------------------------------------*/
        
        //Relations
        
        builder.HasMany(comment => comment.Answers)
               .WithOne(answer => answer.Comment)
               .HasForeignKey(answer => answer.CommentId);
    }
}