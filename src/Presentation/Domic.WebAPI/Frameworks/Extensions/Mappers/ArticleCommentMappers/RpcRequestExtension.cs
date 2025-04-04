using Domic.Core.ArticleComment.Grpc;
using Domic.UseCase.ArticleCommentUseCase.Commands.Active;
using Domic.UseCase.ArticleCommentUseCase.Commands.Create;
using Domic.UseCase.ArticleCommentUseCase.Commands.Delete;
using Domic.UseCase.ArticleCommentUseCase.Commands.InActive;
using Domic.UseCase.ArticleCommentUseCase.Commands.Update;

namespace Domic.WebAPI.Frameworks.Extensions.Mappers.ArticleCommentMappers;

//Query
public static partial class RpcRequestExtension
{
    
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
                ArticleId = request.ArticleId?.Value ,
                Comment   = request.Comment?.Value 
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
                Id      = request.TargetId.Value , 
                Comment = request.Comment?.Value 
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