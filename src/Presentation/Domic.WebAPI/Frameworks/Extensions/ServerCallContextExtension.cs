using System.Collections.ObjectModel;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.WebAPI.Extensions;
using Grpc.Core;

namespace Dotris.WebAPI.Frameworks.Extensions;

public static class ServerCallContextExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="jsonWebToken"></param>
    /// <returns></returns>
    public static (
        string UserId,
        string Username,
        ReadOnlyCollection<string> UserRoles,
        ReadOnlyCollection<string> UserPermissions
    )
    GetAuditInfo(this ServerCallContext context, IJsonWebToken jsonWebToken)
    {
        var token = context.GetHttpContext().GetTokenOfGrpcHeader();

        return (
            jsonWebToken.GetIdentityUserId(token),
            jsonWebToken.GetUsername(token),
            jsonWebToken.GetRoles(token).AsReadOnly(),
            jsonWebToken.GetPermissions(token).AsReadOnly()
        );
    }
}