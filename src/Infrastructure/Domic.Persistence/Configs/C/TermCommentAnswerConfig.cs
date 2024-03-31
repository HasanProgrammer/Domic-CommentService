using Domic.Core.Persistence.Configs;
using Domic.Domain.TermCommentAnswer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domic.Persistence.Configs.C;

public class TermCommentAnswerConfig : BaseEntityConfig<TermCommentAnswer, string>
{
    public override void Configure(EntityTypeBuilder<TermCommentAnswer> builder)
    {
        base.Configure(builder);
        
        /*-----------------------------------------------------------*/

        //Configs

        builder.ToTable("TermCommentAnswerAnswers");
        
        builder.Property(comment => comment.CommentId).IsRequired();
        
        builder.OwnsOne(comment => comment.Answer)
               .Property(comment => comment.Value)
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