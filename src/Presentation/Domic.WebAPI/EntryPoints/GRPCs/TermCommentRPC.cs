using Domic.Core.TermComment.Grpc;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.WebAPI.EntryPoints.GRPCs;

public class TermCommentRPC : TermCommentService.TermCommentServiceBase
{
    private readonly IMediator      _mediator;
    private readonly IConfiguration _configuration;

    public TermCommentRPC(IMediator mediator, IConfiguration configuration)
    {
        _mediator      = mediator;
        _configuration = configuration;
    }
}