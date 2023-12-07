#pragma warning disable CS0649

using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.Domain.Enumerations;
using Karami.Core.Domain.ValueObjects;
using Karami.Domain.ArticleCommentAnswer.Events;
using Karami.Domain.ArticleCommentAnswer.ValueObjects;

namespace Karami.Domain.ArticleCommentAnswer.Entities;

public class ArticleCommentAnswer : Entity<string>
{
    public string OwnerId   { get; private set; }
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
    /// <param name="dotrisDateTime"></param>
    /// <param name="id"></param>
    /// <param name="ownerId"></param>
    /// <param name="commentId"></param>
    /// <param name="answer"></param>
    public ArticleCommentAnswer(IDotrisDateTime dotrisDateTime, string id, string ownerId, string commentId, string answer)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dotrisDateTime.ToPersianShortDate(nowDateTime);

        Id        = id;
        OwnerId   = ownerId;
        CommentId = commentId;
        Answer    = new Answer(answer);
        CreatedAt = new CreatedAt(nowDateTime, nowPersianDateTime);
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentAnswerCreated {
                Id                    = id                 , 
                OwnerId               = ownerId            ,
                CommentId             = commentId          , 
                Answer                = answer             ,
                CreatedAt_EnglishDate = nowDateTime        ,
                UpdatedAt_EnglishDate = nowDateTime        ,
                CreatedAt_PersianDate = nowPersianDateTime ,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }

    /*---------------------------------------------------------------*/
    
    //Behaviors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dotrisDateTime"></param>
    /// <param name="answer"></param>
    public void Change(IDotrisDateTime dotrisDateTime, string answer)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dotrisDateTime.ToPersianShortDate(nowDateTime);
        
        Answer    = new Answer(answer);
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentAnswerUpdated {
                Id                    = Id          ,
                Answer                = answer      ,
                UpdatedAt_EnglishDate = nowDateTime ,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dotrisDateTime"></param>
    /// <param name="raiseEvent"></param>
    public void Active(IDotrisDateTime dotrisDateTime, bool raiseEvent = true)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dotrisDateTime.ToPersianShortDate(nowDateTime);
        
        IsActive  = IsActive.Active;
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new ArticleCommentAnswerActived {
                    Id                    = Id          ,
                    UpdatedAt_EnglishDate = nowDateTime ,
                    UpdatedAt_PersianDate = nowPersianDateTime
                }
            );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dotrisDateTime"></param>
    /// <param name="raiseEvent"></param>
    public void InActive(IDotrisDateTime dotrisDateTime, bool raiseEvent = true)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dotrisDateTime.ToPersianShortDate(nowDateTime);
        
        IsActive  = IsActive.InActive;
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new ArticleCommentAnswerInActived {
                    Id                    = Id          ,
                    UpdatedAt_EnglishDate = nowDateTime ,
                    UpdatedAt_PersianDate = nowPersianDateTime
                }
            );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dotrisDateTime"></param>
    /// <param name="raiseEvent"></param>
    public void Delete(IDotrisDateTime dotrisDateTime, bool raiseEvent = true)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dotrisDateTime.ToPersianShortDate(nowDateTime);

        IsDeleted = IsDeleted.Delete;
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new ArticleCommentAnswerDeleted {
                    Id                    = Id          ,
                    UpdatedAt_EnglishDate = nowDateTime ,
                    UpdatedAt_PersianDate = nowPersianDateTime
                }
            );
    }
}