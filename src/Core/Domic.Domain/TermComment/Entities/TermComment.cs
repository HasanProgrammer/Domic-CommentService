using System.Security.Principal;
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
    /// <param name="identity"></param>
    /// <param name="termId"></param>
    /// <param name="serializer"></param>
    /// <param name="comment"></param>
    public TermComment(IDateTime dateTime, IGlobalUniqueIdGenerator globalUniqueIdGenerator, IIdentityUser identity, string termId, 
        ISerializer serializer, string comment
    )
    {
        var nowDatetime = DateTime.Now;
        var nowPersianDate = dateTime.ToPersianShortDate(nowDatetime);

        Id = globalUniqueIdGenerator.GetRandom(6);
        TermId = termId;
        Comment = new Comment(comment);

        //audit
        CreatedBy = identity.GetIdentity();
        CreatedRole = serializer.Serialize(identity.GetRoles());
        CreatedAt = new CreatedAt(nowDatetime, nowPersianDate);
        
        AddEvent(
            new TermCommentCreated {
                Id = Id,
                TermId = termId,
                Comment = comment,
                CreatedBy = CreatedBy,
                CreatedRole = CreatedRole,
                CreatedAt_EnglishDate = nowDatetime,
                CreatedAt_PersianDate = nowPersianDate
            }
        );
    }
    
    /*---------------------------------------------------------------*/
    
    //Behaviors
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    /// <param name="comment"></param>
    public void Change(IDateTime dateTime, IIdentityUser identityUser, ISerializer serializer, string comment)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Comment = new Comment(comment);

        //audit
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TermCommentUpdated {
                Id                    = Id          ,
                Comment               = comment     ,
                UpdatedBy             = UpdatedBy   ,
                UpdatedRole           = UpdatedRole ,
                UpdatedAt_EnglishDate = nowDateTime ,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    public void InActive(IDateTime dateTime, IIdentityUser identityUser, ISerializer serializer)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsActive = IsActive.InActive;

        //audit
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TermCommentInActived {
                Id                    = Id          ,
                UpdatedBy             = UpdatedBy   ,
                UpdatedRole           = UpdatedRole ,
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

        IsActive = IsActive.InActive;

        //audit
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
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    public void Active(IDateTime dateTime, IIdentityUser identityUser, ISerializer serializer)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsActive = IsActive.Active;

        //audit
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TermCommentActived {
                Id                    = Id          ,
                UpdatedBy             = UpdatedBy   , 
                UpdatedRole           = UpdatedRole , 
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
                new TermCommentActived {
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
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    public void Delete(IDateTime dateTime, IIdentityUser identityUser, ISerializer serializer)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsDeleted = IsDeleted.Delete;

        //audit
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TermCommentDeleted {
                Id                    = Id          ,
                UpdatedBy             = UpdatedBy   , 
                UpdatedRole           = UpdatedRole ,
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