using Grpc.Core;
using Domic.Core.ArticleComment.Grpc;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.WebAPI.Extensions;
using Domic.UseCase.ArticleCommentUseCase.Commands.Active;
using Domic.UseCase.ArticleCommentUseCase.Commands.Create;
using Domic.UseCase.ArticleCommentUseCase.Commands.Delete;
using Domic.UseCase.ArticleCommentUseCase.Commands.InActive;
using Domic.UseCase.ArticleCommentUseCase.Commands.Update;
using Domic.WebAPI.Frameworks.Extensions.Mappers.ArticleCommentMappers;

namespace Domic.WebAPI.EntryPoints.GRPCs;

public class ArticleCommentRPC : ArticleCommentService.ArticleCommentServiceBase
{
    private readonly IMediator      _mediator;
    private readonly IConfiguration _configuration;

    public ArticleCommentRPC(IMediator mediator, IConfiguration configuration)
    {
        _mediator      = mediator;
        _configuration = configuration;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        var command = request.ToCommand<CreateCommand>(context.GetHttpContext().GetTokenOfGrpcHeader());

        var result = await _mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<CreateResponse>(_configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
    {
        var command = request.ToCommand<UpdateCommand>(context.GetHttpContext().GetTokenOfGrpcHeader());
        
        var result = await _mediator.DispatchAsync<string>(command, context.CancellationToken);
        
        return result.ToRpcResponse<UpdateResponse>(_configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<ActiveResponse> Active(ActiveRequest request, ServerCallContext context)
    {
        var command = request.ToCommand<ActiveCommand>(context.GetHttpContext().GetTokenOfGrpcHeader());

        var result = await _mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<ActiveResponse>(_configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<InActiveResponse> InActive(InActiveRequest request, ServerCallContext context)
    {
        var command = request.ToCommand<InActiveCommand>(context.GetHttpContext().GetTokenOfGrpcHeader());

        var result = await _mediator.DispatchAsync<string>(command, context.CancellationToken);

        return result.ToRpcResponse<InActiveResponse>(_configuration);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
    {
        var command = request.ToCommand<DeleteCommand>(context.GetHttpContext().GetTokenOfGrpcHeader());
        
        var result = await _mediator.DispatchAsync<string>(command, context.CancellationToken);
        
        return result.ToRpcResponse<DeleteResponse>(_configuration);
    }
}