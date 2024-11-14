#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Active;

public class ActiveCommandHandler : ICommandHandler<ActiveCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                        _dateTime;
    private readonly ISerializer                      _serializer;
    private readonly IJsonWebToken                    _jsonWebToken;
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;

    public ActiveCommandHandler(IArticleCommentCommandRepository articleCommentCommandRepository, 
        IDateTime dateTime, ISerializer serializer, IJsonWebToken jsonWebToken
    )
    {
        _dateTime                        = dateTime;
        _serializer                      = serializer;
        _jsonWebToken                    = jsonWebToken;
        _articleCommentCommandRepository = articleCommentCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(ActiveCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as ArticleComment;
        var updatedBy     = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole   = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        targetComment.Active(_dateTime, updatedBy, updatedRole);

        _articleCommentCommandRepository.Change(targetComment);

        return Task.FromResult(targetComment.Id);
    }

    public Task AfterTransactionHandleAsync(ActiveCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}