using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;
using Domic.Core.Domain.ValueObjects;
using Domic.Domain.Commons.ValueObjects;
using Domic.Domain.TermComment.Events;

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
    
    public ICollection<TermCommentAnswer.Entities.TermCommentAnswer> Answers { get; set; }
    
    /*---------------------------------------------------------------*/

    //EF Core
    public TermComment(){}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="globalUniqueIdGenerator"></param>
    /// <param name="termId"></param>
    /// <param name="createdBy"></param>
    /// <param name="createdRole"></param>
    /// <param name="comment"></param>
    public TermComment(IDateTime dateTime, IGlobalUniqueIdGenerator globalUniqueIdGenerator, string termId, 
        string createdBy, string createdRole, string comment
    )
    {
        var nowDatetime = DateTime.Now;
        var nowPersianDate = dateTime.ToPersianShortDate(nowDatetime);

        Id = globalUniqueIdGenerator.GetRandom(6);
        TermId = termId;
        Comment = new Comment(comment);
        CreatedBy = createdBy;
        CreatedRole = createdRole;
        CreatedAt = new CreatedAt(nowDatetime, nowPersianDate);
        
        AddEvent(
            new TermCommentCreated {
                Id = Id,
                TermId = termId,
                Comment = comment,
                CreatedBy = createdBy,
                CreatedRole = createdRole,
                CreatedAt_EnglishDate = nowDatetime,
                CreatedAt_PersianDate = nowPersianDate
            }
        );
    }
    
    /*---------------------------------------------------------------*/
    
    //Behaviors
    
    public void Change(IDateTime dateTime, string updatedBy, string updatedRole, string comment)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        Comment     = new Comment(comment);
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TermCommentUpdated {
                Id                    = Id          ,
                Comment               = comment     ,
                UpdatedBy             = updatedBy   ,
                UpdatedRole           = updatedRole ,
                UpdatedAt_EnglishDate = nowDateTime ,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="updatedBy"></param>
    /// <param name="updatedRole"></param>
    /// <param name="raiseEvent"></param>
    public void InActive(IDateTime dateTime, string updatedBy, string updatedRole, bool raiseEvent = true)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsActive    = IsActive.InActive;
        UpdatedRole = updatedRole;
        UpdatedBy   = updatedBy;
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new TermCommentInActived {
                    Id                    = Id          ,
                    UpdatedBy             = updatedBy   ,
                    UpdatedRole           = updatedRole ,
                    UpdatedAt_EnglishDate = nowDateTime ,
                    UpdatedAt_PersianDate = nowPersianDateTime
                }
            );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="updatedBy"></param>
    /// <param name="updatedRole"></param>
    /// <param name="raiseEvent"></param>
    public void Active(IDateTime dateTime, string updatedBy, string updatedRole, bool raiseEvent = true)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsActive    = IsActive.Active;
        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new TermCommentInActived {
                    Id                    = Id          ,
                    UpdatedBy             = updatedBy   , 
                    UpdatedRole           = updatedRole , 
                    UpdatedAt_EnglishDate = nowDateTime ,
                    UpdatedAt_PersianDate = nowPersianDateTime
                }
            );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="updatedBy"></param>
    /// <param name="updatedRole"></param>
    /// <param name="raiseEvent"></param>
    public void Delete(IDateTime dateTime, string updatedBy, string updatedRole, bool raiseEvent = true)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsDeleted   = IsDeleted.Delete;
        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new TermCommentDeleted {
                    Id                    = Id          ,
                    UpdatedBy             = updatedBy   , 
                    UpdatedRole           = updatedRole ,
                    UpdatedAt_EnglishDate = nowDateTime ,
                    UpdatedAt_PersianDate = nowPersianDateTime
                }
            );
    }
}