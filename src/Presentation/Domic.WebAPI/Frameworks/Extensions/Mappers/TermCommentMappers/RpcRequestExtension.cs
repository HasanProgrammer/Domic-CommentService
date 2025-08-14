using Domic.Core.TermComment.Grpc;
using Domic.UseCase.TermCommentUseCase.Commands.Active;
using Domic.UseCase.TermCommentUseCase.Commands.Create;
using Domic.UseCase.TermCommentUseCase.Commands.Delete;
using Domic.UseCase.TermCommentUseCase.Commands.InActive;
using Domic.UseCase.TermCommentUseCase.Commands.Update;

namespace Domic.WebAPI.Frameworks.Extensions.Mappers.TermCommentMappers;

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
    /// <returns></returns>
    public static CreateCommand ToCommand(this CreateRequest request) => new() {
        TermId  = request.TermId?.Value,
        Comment = request.Comment?.Value
    };

    /// <summary>
    /// Map UpdateRequest to UpdateCommand
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static UpdateCommand ToCommand(this UpdateRequest request) => new() {
        Id      = request.CommentId.Value,
        Comment = request.Comment.Value
    };

    /// <summary>
    /// Map ActiveRequest to ActiveCommand
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static ActiveCommand ToCommand(this ActiveRequest request) => new() {
        Id = request.CommentId.Value
    };

    /// <summary>
    /// Map InActiveRequest to InActiveCommand
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static InActiveCommand ToCommand(this InActiveRequest request) => new() {
        Id = request.CommentId.Value
    };

    /// <summary>
    /// Map DeleteRequest to DeleteCommand
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static DeleteCommand ToCommand(this DeleteRequest request) => new() {
        Id = request.CommentId.Value
    };
}