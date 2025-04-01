using System.Collections.ObjectModel;
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
        TermId = request.TermId.Value,
        Comment = request.Comment.Value
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
        Id = request.CommentId.Value,
        Comment = request.Comment.Value
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
        Id = request.CommentId.Value
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
        Id = request.CommentId.Value
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
        Id = request.CommentId.Value
    };
}