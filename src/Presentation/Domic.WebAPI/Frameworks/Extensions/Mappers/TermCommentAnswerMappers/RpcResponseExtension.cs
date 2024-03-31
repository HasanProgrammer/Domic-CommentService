using Domic.Core.Common.ClassExtensions;
using Domic.Core.TermCommentAnswer.Grpc;

namespace Domic.WebAPI.Frameworks.Extensions.Mappers.TermCommentAnswerMappers;

//Query
public static partial class RpcResponseExtension
{
    
}

//Command
public static partial class RpcResponseExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="response"></param>
    /// <param name="configuration"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ToRpcResponse<T>(this string response, IConfiguration configuration)
    {
        object Response = null;

        if (typeof(T) == typeof(CreateResponse))
        {
            Response = new CreateResponse {
                Code    = configuration.GetSuccessCreateStatusCode() ,
                Message = configuration.GetSuccessCreateMessage()    ,
                Body    = new CreateResponseBody { AnswerId = response }
            };
        }
        else if (typeof(T) == typeof(UpdateResponse))
        {
            Response = new UpdateResponse {
                Code    = configuration.GetSuccessStatusCode()    ,
                Message = configuration.GetSuccessUpdateMessage() ,
                Body    = new UpdateResponseBody { AnswerId = response }
            };
        }
        else if (typeof(T) == typeof(ActiveResponse))
        {
            Response = new ActiveResponse {
                Code    = configuration.GetSuccessStatusCode()    ,
                Message = configuration.GetSuccessUpdateMessage() ,
                Body    = new ActiveResponseBody { AnswerId = response }
            };
        }
        else if (typeof(T) == typeof(InActiveResponse))
        {
            Response = new InActiveResponse {
                Code    = configuration.GetSuccessStatusCode()    ,
                Message = configuration.GetSuccessUpdateMessage() ,
                Body    = new InActiveResponseBody { AnswerId = response }
            };
        }
        else if (typeof(T) == typeof(DeleteResponse))
        {
            Response = new DeleteResponse {
                Code    = configuration.GetSuccessStatusCode()    ,
                Message = configuration.GetSuccessDeleteMessage() ,
                Body    = new DeleteResponseBody { AnswerId = response }
            };
        }

        return (T)Response;
    }
}