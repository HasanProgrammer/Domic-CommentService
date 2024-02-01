using Karami.Core.Persistence.Configs;
using Karami.Domain.ArticleCommentAnswer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Karami.Persistence.Configs.C;

public class ArticleCommentAnswerConfig : BaseEntityConfig<ArticleCommentAnswer, string>
{
    public override void Configure(EntityTypeBuilder<ArticleCommentAnswer> builder)
    {
        base.Configure(builder);
        
        /*-----------------------------------------------------------*/

        //Configs

        builder.ToTable("ArticleCommentAnswers");
        
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
    }
}