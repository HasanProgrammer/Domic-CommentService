using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;
using Domic.Core.Domain.ValueObjects;
using Domic.Domain.Commons.ValueObjects;
using Domic.Domain.TermCommentAnswer.Events;

namespace Domic.Domain.TermCommentAnswer.Entities;

public class TermCommentAnswer : Entity<string>
{
    //Fields
    
    public string CommentId { get; private set; }
    
    /*---------------------------------------------------------------*/
    
    //Value Objects
    
    public Answer Answer { get; private set; }
    
    /*---------------------------------------------------------------*/
    
    //Relations
    
    public TermComment.Entities.TermComment Comment { get; set; }
    
    /*---------------------------------------------------------------*/
    
    //EF Core
    private TermCommentAnswer() {}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="globalUniqueIdGenerator"></param>
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    /// <param name="commentId"></param>
    /// <param name="answer"></param>
    public TermCommentAnswer(IDateTime dateTime, IGlobalUniqueIdGenerator globalUniqueIdGenerator, IIdentityUser identityUser, 
        ISerializer serializer, string commentId, string answer
    )
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id          = globalUniqueIdGenerator.GetRandom(6);
        CommentId   = commentId;
        Answer      = new Answer(answer);

        //audit
        CreatedBy   = identityUser.GetIdentity();
        CreatedRole = serializer.Serialize(identityUser.GetRoles());
        CreatedAt   = new CreatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TermCommentAnswerCreated {
                Id                    = Id                 , 
                CommentId             = commentId          , 
                Answer                = answer             ,
                CreatedBy             = CreatedBy          ,
                CreatedRole           = CreatedRole        ,
                CreatedAt_EnglishDate = nowDateTime        ,
                CreatedAt_PersianDate = nowPersianDateTime
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
    /// <param name="answer"></param>
    public void Change(IDateTime dateTime, IIdentityUser identityUser, ISerializer serializer, string answer)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Answer = new Answer(answer);

        //audit
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TermCommentAnswerUpdated {
                Id                    = Id          ,
                Answer                = answer      ,
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
    /// <param name="answer"></param>
    public void Change(IDateTime dateTime, string updatedBy, string updatedRole, string answer)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Answer      = new Answer(answer);
        UpdatedRole = updatedRole;
        UpdatedBy   = updatedBy;
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TermCommentAnswerUpdated {
                Id                    = Id          ,
                Answer                = answer      ,
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
    /// <param name="raiseEvent"></param>
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
            new TermCommentAnswerActived {
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
                new TermCommentAnswerActived {
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
            new TermCommentAnswerInActived {
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

        IsActive    = IsActive.InActive;
        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole; 
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new TermCommentAnswerInActived {
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
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TermCommentAnswerDeleted {
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
                new TermCommentAnswerDeleted {
                    Id                    = Id          ,
                    UpdatedBy             = updatedBy   ,
                    UpdatedRole           = updatedRole , 
                    UpdatedAt_EnglishDate = nowDateTime ,
                    UpdatedAt_PersianDate = nowPersianDateTime
                }
            );
    }
}