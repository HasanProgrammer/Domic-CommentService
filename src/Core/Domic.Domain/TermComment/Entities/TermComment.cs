using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Domain.Commons.ValueObjects;

namespace Domic.Domain.TermComment.Entities;

public class TermComment : Entity<string>
{
    //Fields
    
    public string TermId { get; private set; }
    
    /*---------------------------------------------------------------*/
    
    //Value Objects
    
    public Comment Comment { get; private set; }
    
    /*---------------------------------------------------------------*/
    
    //Relations
}