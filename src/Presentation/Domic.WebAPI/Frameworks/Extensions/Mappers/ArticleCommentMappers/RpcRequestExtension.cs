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
                ArticleId = request.ArticleId?.Value ,
                Comment   = request.Comment?.Value 
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
                Comment  = request.Comment?.Value 
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