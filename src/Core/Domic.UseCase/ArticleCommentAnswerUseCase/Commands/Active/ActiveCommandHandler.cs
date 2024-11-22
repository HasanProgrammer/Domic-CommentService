#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Active;

public class ActiveCommandHandler : ICommandHandler<ActiveCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                              _dateTime;
    private readonly ISerializer                            _serializer;
    private readonly IJsonWebToken                          _jsonWebToken;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public ActiveCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, 
        IDateTime dateTime, IJsonWebToken jsonWebToken, ISerializer serializer
    )
    {
        _dateTime                              = dateTime;
        _jsonWebToken                          = jsonWebToken;
        _serializer                            = serializer;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    public Task BeforeHandleAsync(ActiveCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(ActiveCommand command, CancellationToken cancellationToken)
    {
        var answer      = _validationResult as ArticleCommentAnswer;
        var updatedBy   = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        answer.Active(_dateTime, updatedBy, updatedRole);

        _articleCommentAnswerCommandRepository.Change(answer);

        return Task.FromResult(answer.Id);
    }

    public Task AfterHandleAsync(ActiveCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}