using Domic.Core.Persistence.Configs;
using Domic.Domain.TermComment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domic.Persistence.Configs.C;

public class TermCommentConfig : BaseEntityConfig<TermComment, string>
{
    public override void Configure(EntityTypeBuilder<TermComment> builder)
    {
        base.Configure(builder);
        
        /*-----------------------------------------------------------*/

        //Configs

        builder.ToTable("TermComments");
        
        builder.Property(comment => comment.TermId).IsRequired();
        
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