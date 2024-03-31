﻿using Domic.Core.TermComment.Grpc;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.WebAPI.Frameworks.Extensions.Mappers.TermCommentMappers;
using Dotris.WebAPI.Frameworks.Extensions;
using Grpc.Core;

namespace Domic.WebAPI.EntryPoints.GRPCs;

public class TermCommentRPC(IMediator mediator, IConfiguration configuration, IJsonWebToken jsonWebToken)
    : TermCommentService.TermCommentServiceBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        var auditInfo = context.GetAuditInfo(jsonWebToken);

        var command = request.ToCommand(auditInfo.UserId, auditInfo.Username, auditInfo.UserRoles,
            auditInfo.UserPermissions
        );
        
        var result = await mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<CreateResponse>(configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
    { 
        var auditInfo = context.GetAuditInfo(jsonWebToken);
        
        var command = request.ToCommand(auditInfo.UserId, auditInfo.Username, auditInfo.UserRoles,
            auditInfo.UserPermissions
        );

        var result = await mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<UpdateResponse>(configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ActiveResponse> Active(ActiveRequest request, ServerCallContext context)
    {
        var auditInfo = context.GetAuditInfo(jsonWebToken);
        
        var command = request.ToCommand(auditInfo.UserId, auditInfo.Username, auditInfo.UserRoles,
            auditInfo.UserPermissions
        );

        var result = await mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<ActiveResponse>(configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<InActiveResponse> InActive(InActiveRequest request, ServerCallContext context)
    {
        var auditInfo = context.GetAuditInfo(jsonWebToken);
        
        var command = request.ToCommand(auditInfo.UserId, auditInfo.Username, auditInfo.UserRoles,
            auditInfo.UserPermissions
        );

        var result = await mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<InActiveResponse>(configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
    {
        var auditInfo = context.GetAuditInfo(jsonWebToken);
        
        var command = request.ToCommand(auditInfo.UserId, auditInfo.Username, auditInfo.UserRoles,
            auditInfo.UserPermissions
        );

        var result = await mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<DeleteResponse>(configuration);
    }
}