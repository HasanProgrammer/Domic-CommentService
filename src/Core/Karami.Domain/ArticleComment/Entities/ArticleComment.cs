using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.Domain.Enumerations;
using Karami.Core.Domain.ValueObjects;
using Karami.Domain.ArticleComment.ValueObjects;
using Karami.Domain.ArticleComment.Events;

namespace Karami.Domain.ArticleComment.Entities;

public class ArticleComment : Entity<string>
{
    public string OwnerId   { get; private set; }
    public string ArticleId { get; private set; }
    
    /*---------------------------------------------------------------*/
    
    //Value Objects
    
    public Comment Comment { get; private set; }

    /*---------------------------------------------------------------*/
    
    //Relations
    
    public ICollection<ArticleCommentAnswer.Entities.ArticleCommentAnswer> Answers { get; set; }

    /*---------------------------------------------------------------*/

    //EF Core
    private ArticleComment() {}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dotrisDateTime"></param>
    /// <param name="id"></param>
    /// <param name="ownerId"></param>
    /// <param name="articleId"></param>
    /// <param name="comment"></param>
    public ArticleComment(IDotrisDateTime dotrisDateTime, string id, string ownerId, string articleId, string comment)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dotrisDateTime.ToPersianShortDate(nowDateTime);

        Id        = id;
        OwnerId   = ownerId;
        ArticleId = articleId;
        Comment   = new Comment(comment);
        CreatedAt = new CreatedAt(nowDateTime, nowPersianDateTime);
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentCreated {
                Id                    = id                 ,
                OwnerId               = ownerId            ,
                ArticleId             = articleId          ,
                Comment               = comment            ,
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
    /// <param name="comment"></param>
    public void Change(IDotrisDateTime dotrisDateTime, string comment)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dotrisDateTime.ToPersianShortDate(nowDateTime);

        Comment   = new Comment(comment);
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentUpdated {
                Id                    = Id          , 
                Comment               = comment     ,
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
                new ArticleCommentInActived {
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
    public void Active(IDotrisDateTime dotrisDateTime, bool raiseEvent = true)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dotrisDateTime.ToPersianShortDate(nowDateTime);

        IsActive  = IsActive.Active;
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new ArticleCommentInActived {
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
                new ArticleCommentDeleted {
                    Id                    = Id          ,
                    UpdatedAt_EnglishDate = nowDateTime ,
                    UpdatedAt_PersianDate = nowPersianDateTime
                }
            );
    }
}