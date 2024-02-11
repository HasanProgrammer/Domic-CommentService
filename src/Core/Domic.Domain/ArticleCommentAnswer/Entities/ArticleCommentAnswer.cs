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
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="id"></param>
    /// <param name="createdBy"></param>
    /// <param name="createdRole"></param>
    /// <param name="commentId"></param>
    /// <param name="answer"></param>
    public ArticleCommentAnswer(IDateTime dateTime, string id, string createdBy, string createdRole, 
        string commentId, string answer
    )
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id          = id;
        CreatedBy   = createdBy;
        CommentId   = commentId;
        CreatedRole = createdRole;
        Answer      = new Answer(answer);
        CreatedAt   = new CreatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentAnswerCreated {
                Id                    = id                 , 
                CreatedBy             = createdBy          ,
                CreatedRole           = createdRole        ,
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
    /// <param name="updatedBy"></param>
    /// <param name="updatedRole"></param>
    /// <param name="answer"></param>
    public void Change(IDateTime dateTime, string updatedBy, string updatedRole, string answer)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        Answer      = new Answer(answer);
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentAnswerUpdated {
                Id                    = Id          ,
                UpdatedBy             = updatedBy   ,
                UpdatedRole           = updatedRole , 
                Answer                = answer      ,
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
        
        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        IsActive    = IsActive.Active;
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

        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole; 
        IsActive    = IsActive.InActive;
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

        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        IsDeleted   = IsDeleted.Delete;
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