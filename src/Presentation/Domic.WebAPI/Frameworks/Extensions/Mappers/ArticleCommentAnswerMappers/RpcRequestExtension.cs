using Domic.Core.ArticleCommentAnswer.Grpc;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Active;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.CheckExist;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Create;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Delete;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.InActive;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Update;

namespace Domic.WebAPI.Frameworks.Extensions.Mappers.ArticleCommentAnswerMappers;

//Query
public static partial class RpcRequestExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToQuery<T>(this CheckExistRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(CheckExistCommand))
        {
            Request = new CheckExistCommand {
                Id = request.TargetId?.Value
            };
        }

        return (T)Request;
    }
}

//Command
public static partial class RpcRequestExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this CreateRequest request, string token)
    {
        object Request = null;

        if (typeof(T) == typeof(CreateCommand))
        {
            Request = new CreateCommand {
                Token     = token                    , 
                OwnerId   = request.OwnerId?.Value   ,
                CommentId = request.CommentId?.Value ,
                Answer    = request.Answer?.Value 
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this UpdateRequest request, string token)
    {
        object Request = null;

        if (typeof(T) == typeof(UpdateCommand))
        {
            Request = new UpdateCommand {
                Token    = token                  ,
                TargetId = request.TargetId.Value , 
                Answer   = request.Answer?.Value 
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this ActiveRequest request, string token)
    {
        object Request = null;

        if (typeof(T) == typeof(ActiveCommand))
        {
            Request = new ActiveCommand {
                Token    = token ,
                Id = request.TargetId.Value
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this InActiveRequest request, string token)
    {
        object Request = null;

        if (typeof(T) == typeof(InActiveCommand))
        {
            Request = new InActiveCommand {
                Token    = token ,
                TargetId = request.TargetId.Value
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this DeleteRequest request, string token)
    {
        object Request = null;

        if (typeof(T) == typeof(DeleteCommand))
        {
            Request = new DeleteCommand {
                Token    = token ,
                TargetId = request.TargetId.Value
            };
        }

        return (T)Request;
    }
}