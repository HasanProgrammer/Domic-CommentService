using Domic.Domain.ArticleComment.Events;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;
using Domic.Core.Domain.ValueObjects;
using Domic.Domain.Commons.ValueObjects;

namespace Domic.Domain.ArticleComment.Entities;

public class ArticleComment : Entity<string>
{
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
    /// <param name="dateTime"></param>
    /// <param name="globalUniqueIdGenerator"></param>
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    /// <param name="articleId"></param>
    /// <param name="comment"></param>
    public ArticleComment(IDateTime dateTime, IGlobalUniqueIdGenerator globalUniqueIdGenerator, IIdentityUser identityUser, ISerializer serializer,
        string articleId, string comment
    )
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id        = globalUniqueIdGenerator.GetRandom(6);
        ArticleId = articleId;
        Comment   = new Comment(comment);

        //audit
        CreatedBy   = identityUser.GetIdentity();
        CreatedRole = serializer.Serialize(identityUser.GetRoles());
        CreatedAt   = new CreatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentCreated {
                Id                    = Id                 ,
                CreatedBy             = CreatedBy          ,
                ArticleId             = articleId          ,
                CreatedRole           = CreatedRole        ,
                Comment               = comment            ,
                CreatedAt_EnglishDate = nowDateTime        ,
                CreatedAt_PersianDate = nowPersianDateTime
            }
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="id"></param>
    /// <param name="createdBy"></param>
    /// <param name="articleId"></param>
    /// <param name="createdRole"></param>
    /// <param name="comment"></param>
    public ArticleComment(IDateTime dateTime, string id, string createdBy, string articleId, string createdRole,
        string comment
    )
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id        = id;
        ArticleId = articleId;
        Comment   = new Comment(comment);

        //audit
        CreatedBy   = createdBy;
        CreatedRole = createdRole;
        CreatedAt   = new CreatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentCreated {
                Id                    = id                 ,
                CreatedBy             = createdBy          ,
                ArticleId             = articleId          ,
                CreatedRole           = createdBy          ,
                Comment               = comment            ,
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
            new ArticleCommentUpdated {
                Id                    = Id          ,
                UpdatedBy             = UpdatedBy   , 
                UpdatedRole           = UpdatedRole , 
                Comment               = comment     ,
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
    /// <param name="comment"></param>
    public void Change(IDateTime dateTime, string updatedBy, string updatedRole, string comment)
    {
        var nowDateTime        = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        Comment     = new Comment(comment);
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentUpdated {
                Id                    = Id          ,
                UpdatedBy             = updatedBy   , 
                UpdatedRole           = updatedRole , 
                Comment               = comment     ,
                UpdatedAt_EnglishDate = nowDateTime ,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }

    /// <summary>
    /// dd      
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
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentInActived {
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
        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new ArticleCommentInActived {
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
        UpdatedBy   = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new ArticleCommentActived {
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
                new ArticleCommentInActived {
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
            new ArticleCommentDeleted {
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

        UpdatedBy   = updatedBy;
        UpdatedRole = updatedRole;
        IsDeleted   = IsDeleted.Delete;
        UpdatedAt   = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if(raiseEvent)
            AddEvent(
                new ArticleCommentDeleted {
                    Id                    = Id          ,
                    UpdatedBy             = updatedBy   , 
                    UpdatedRole           = updatedRole ,
                    UpdatedAt_EnglishDate = nowDateTime ,
                    UpdatedAt_PersianDate = nowPersianDateTime
                }
            );
    }
}