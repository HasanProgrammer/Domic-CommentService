using Karami.Core.Persistence.Configs;
using Karami.Domain.ArticleCommentAnswer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karami.Persistence.Configs.C;

public class ArticleCommentAnswerConfig : IEntityTypeConfiguration<ArticleCommentAnswer>
{
    public void Configure(EntityTypeBuilder<ArticleCommentAnswer> builder)
    {
        //PrimaryKey
        
        builder.ToTable("ArticleCommentAnswers");

        /*-----------------------------------------------------------*/

        //Property

        builder.Property(answer => answer.OwnerId)  .IsRequired();
        builder.Property(answer => answer.CommentId).IsRequired();
        
        builder.OwnsOne(answer => answer.Answer)
               .Property(answer => answer.Value)
               .IsRequired()
               .HasMaxLength(800)
               .HasColumnName("Answer");

        /*-----------------------------------------------------------*/
        
        //Relations
        
        builder.HasOne(answer => answer.Comment)
               .WithMany(comment => comment.Answers)
               .HasForeignKey(answer => answer.CommentId);
        
        /*-----------------------------------------------------------*/
        
        //Configs
        
        new BaseEntityConfig<ArticleCommentAnswer, string>().Configure(builder);
    }
}