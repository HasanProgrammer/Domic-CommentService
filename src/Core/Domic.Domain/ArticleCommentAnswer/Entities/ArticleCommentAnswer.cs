#pragma warning disable CS0649

using Domic.Domain.ArticleCommentAnswer.Events;
using Domic.Domain.ArticleCommentAnswer.ValueObjects;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;
using Domic.Core.Domain.ValueObjects;

namespace Domic.Domain.ArticleCommentAnswer.Entities;

public class ArticleCommentAnswer : Entity<string>
{
    //Fields
    
    public string CommentId { get; private set; }
    
    /*---------------------------------------------------------------*/
    
    //Value Objects
    
    public Answer Answer { get; private set; }

    /*---------------------------------------------------------------*/
    
    //Relations
    
    public ArticleComment.Entities.ArticleComment Comment { get; set; }

    /*---------------------------------------------------------------*/

    //EF Core
    private ArticleCommentAnswer() {}

    /// <summary>
    /// Create new ArticleCommentAnswer entity
    /// </summary>
    /// <param name="globalUniqueIdGenerator"></param>
    /// <param name="dateTime"></param>
    /// <param name="serializer"></param>
    /// <param name="identityUser"></param>
    /// <param name="commentId"></param>
    /// <param name="answer"></param>
    public ArticleCommentAnswer(IGlobalUniqueIdGenerator globalUniqueIdGenerator, IDateTime dateTime, 
        ISerializer serializer, IIdentityUser identityUser, string commentId, string answer
    )
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id          = globalUniqueIdGenerator.GetRandom(6);
        CommentId   = commentId;
        Answer      = new Answer(answer);
        
        //audit
        CreatedAt   = new CreatedAt(nowDateTime, nowPersianDateTime);
        CreatedBy   = identityUser.GetIdentity();
        CreatedRole = serializer.Serialize(identityUser.GetRoles());
        
        AddEvent(
            new ArticleCommentAnswerCreated {
                Id                    = Id                 , 
                CreatedBy             = CreatedBy          ,
                CreatedRole           = CreatedRole        ,
                CommentId             = commentId          , 
                Answer                = answer             ,
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
    /// <param name="serializer"></param>
    /// <param name="identityUser"></param>
    /// <param name="answer"></param>
    public void Change(IDateTime dateTime, ISerializer serializer, IIdentityUser identityUser, string answer)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Answer = new Answer(answer);
        
        //audit
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentAnswerUpdated {
                Id                    = Id          ,
                UpdatedBy             = UpdatedBy   ,
                UpdatedRole           = UpdatedRole , 
                Answer                = answer      ,
                UpdatedAt_EnglishDate = nowDateTime ,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
    
    public void Active(IDateTime dateTime, ISerializer serializer, IIdentityUser identityUser)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);
        
        IsActive = IsActive.Active;
        
        //audit
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentAnswerActived {
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
        
        IsActive = IsActive.Active;
        
        //audit
        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new ArticleCommentAnswerActived {
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
    public void InActive(IDateTime dateTime, string updatedBy, string updatedRole, bool raiseEvent = true)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsActive = IsActive.InActive;
        
        //audit
        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole; 
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new ArticleCommentAnswerInActived {
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

        IsDeleted = IsDeleted.Delete;
        
        //audit
        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new ArticleCommentAnswerDeleted {
                    Id                    = Id          ,
                    UpdatedBy             = updatedBy   ,
                    UpdatedRole           = updatedRole , 
                    UpdatedAt_EnglishDate = nowDateTime ,
                    UpdatedAt_PersianDate = nowPersianDateTime
                }
            );
    }
}