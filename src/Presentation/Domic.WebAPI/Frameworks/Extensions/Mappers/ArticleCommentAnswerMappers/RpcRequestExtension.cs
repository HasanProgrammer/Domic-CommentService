using Domic.Core.ArticleCommentAnswer.Grpc;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Active;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.CheckExist;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Create;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Delete;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.InActive;
using Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Update;

namespace Domic.WebAPI.Frameworks.Extensions.Mappers.ArticleCommentAnswerMappers;

//Query
public static partial class  RpcRequestExtension
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
    /// Map CreateRequest to CreateCommand
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this CreateRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(CreateCommand))
        {
            Request = new CreateCommand {
                CommentId = request.CommentId?.Value ,
                Answer    = request.Answer?.Value 
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// Map UpdateRequest to UpdateCommand
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this UpdateRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(UpdateCommand))
        {
            Request = new UpdateCommand {
                Id     = request.TargetId.Value , 
                Answer = request.Answer?.Value 
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// Map ActiveRequest to ActiveCommand
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this ActiveRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(ActiveCommand))
        {
            Request = new ActiveCommand {
                Id = request.TargetId.Value
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// Map InActiveRequest to InActiveCommand
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this InActiveRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(InActiveCommand))
        {
            Request = new InActiveCommand {
                Id = request.TargetId.Value
            };
        }

        return (T)Request;
    }
    
    /// <summary>
    /// Map DeleteRequest to DeleteCommand
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToCommand<T>(this DeleteRequest request)
    {
        object Request = null;

        if (typeof(T) == typeof(DeleteCommand))
        {
            Request = new DeleteCommand {
                Id = request.TargetId.Value
            };
        }

        return (T)Request;
    }
}