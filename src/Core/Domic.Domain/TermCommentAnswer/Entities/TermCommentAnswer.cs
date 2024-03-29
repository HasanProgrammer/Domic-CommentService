using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Domain.Commons.ValueObjects;

namespace Domic.Domain.TermCommentAnswer.Entities;

public class TermCommentAnswer : Entity<string>
{
    //Fields
    
    public string TermCommentId { get; private set; }
    
    /*---------------------------------------------------------------*/
    
    //Value Objects
    
    public Answer Answer { get; private set; }
    
    /*---------------------------------------------------------------*/
    
    //Relations
    
    public TermComment.Entities.TermComment TermComment { get; set; }
}