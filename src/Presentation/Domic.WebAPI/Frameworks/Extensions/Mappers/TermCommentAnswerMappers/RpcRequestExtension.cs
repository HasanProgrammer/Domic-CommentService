using Domic.Core.TermCommentAnswer.Grpc;
using Domic.UseCase.TermCommentAnswerUseCase.Commands.Active;
using Domic.UseCase.TermCommentAnswerUseCase.Commands.Create;
using Domic.UseCase.TermCommentAnswerUseCase.Commands.Delete;
using Domic.UseCase.TermCommentAnswerUseCase.Commands.InActive;
using Domic.UseCase.TermCommentAnswerUseCase.Commands.Update;

namespace Domic.WebAPI.Frameworks.Extensions.Mappers.TermCommentAnswerMappers;

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
        CommentId = request.CommentId.Value,
        Answer    = request.Answer.Value
    };

    /// <summary>
    /// Map UpdateRequest to UpdateCommand
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static UpdateCommand ToCommand(this UpdateRequest request) => new() {
        AnswerId = request.AnswerId.Value,
        Answer   = request.Answer.Value
    };

    /// <summary>
    /// Map ActiveRequest to ActiveCommand
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static ActiveCommand ToCommand(this ActiveRequest request) => new() {
        Id = request.AnswerId.Value
    };

    /// <summary>
    /// Map InActiveRequest to InActiveCommand
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static InActiveCommand ToCommand(this InActiveRequest request) => new() {
        Id = request.AnswerId.Value
    };

    /// <summary>
    /// Map DeleteRequest to DeleteCommand
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static DeleteCommand ToCommand(this DeleteRequest request) => new() {
        Id = request.AnswerId.Value
    };
}