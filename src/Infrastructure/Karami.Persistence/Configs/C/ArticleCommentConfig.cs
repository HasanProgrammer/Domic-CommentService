using Karami.Core.Persistence.Configs;
using Karami.Domain.ArticleComment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karami.Persistence.Configs.C;

public class ArticleCommentConfig : IEntityTypeConfiguration<ArticleComment>
{
    public void Configure(EntityTypeBuilder<ArticleComment> builder)
    {
        //PrimaryKey
        
        builder.ToTable("ArticleComments");

        /*-----------------------------------------------------------*/

        //Property

        builder.Property(comment => comment.OwnerId)  .IsRequired();
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
        
        /*-----------------------------------------------------------*/
        
        //Configs
        
        new BaseEntityConfig<ArticleComment, string>().Configure(builder);
    }
}