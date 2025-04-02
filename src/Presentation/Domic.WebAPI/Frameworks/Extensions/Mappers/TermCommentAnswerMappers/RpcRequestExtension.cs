using System.Collections.ObjectModel;
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
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userId"></param>
    /// <param name="username"></param>
    /// <param name="userRoles"></param>
    /// <param name="userPermissions"></param>
    /// <returns></returns>
    public static CreateCommand ToCommand(this CreateRequest request, string userId, string username,
        ReadOnlyCollection<string> userRoles,
        ReadOnlyCollection<string> userPermissions
    ) => new() {
        UserId = userId,
        Username = username,
        UserRoles = userRoles,
        UserPermissions = userPermissions,
        CommentId = request.CommentId.Value,
        Answer = request.Answer.Value
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userId"></param>
    /// <param name="username"></param>
    /// <param name="userRoles"></param>
    /// <param name="userPermissions"></param>
    /// <returns></returns>
    public static UpdateCommand ToCommand(this UpdateRequest request, string userId, string username,
        ReadOnlyCollection<string> userRoles, ReadOnlyCollection<string> userPermissions
    ) => new() {
        UserId = userId,
        Username = username,
        UserRoles = userRoles,
        UserPermissions = userPermissions,
        AnswerId = request.AnswerId.Value,
        Answer = request.Answer.Value
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userId"></param>
    /// <param name="username"></param>
    /// <param name="userRoles"></param>
    /// <param name="userPermissions"></param>
    /// <returns></returns>
    public static ActiveCommand ToCommand(this ActiveRequest request, string userId, string username,
        ReadOnlyCollection<string> userRoles,
        ReadOnlyCollection<string> userPermissions
    ) => new() {
        UserId = userId,
        Username = username,
        UserRoles = userRoles,
        UserPermissions = userPermissions,
        Id = request.AnswerId.Value
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userId"></param>
    /// <param name="username"></param>
    /// <param name="userRoles"></param>
    /// <param name="userPermissions"></param>
    /// <returns></returns>
    public static InActiveCommand ToCommand(this InActiveRequest request, string userId, string username,
        ReadOnlyCollection<string> userRoles,
        ReadOnlyCollection<string> userPermissions
    ) => new() {
        UserId = userId,
        Username = username,
        UserRoles = userRoles,
        UserPermissions = userPermissions,
        AnswerId = request.AnswerId.Value
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userId"></param>
    /// <param name="username"></param>
    /// <param name="userRoles"></param>
    /// <param name="userPermissions"></param>
    /// <returns></returns>
    public static DeleteCommand ToCommand(this DeleteRequest request, string userId, string username,
        ReadOnlyCollection<string> userRoles,
        ReadOnlyCollection<string> userPermissions
    ) => new() {
        UserId = userId,
        Username = username,
        UserRoles = userRoles,
        UserPermissions = userPermissions,
        Id = request.AnswerId.Value
    };
}