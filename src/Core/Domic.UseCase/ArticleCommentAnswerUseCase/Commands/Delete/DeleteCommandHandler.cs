#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Delete;

public class DeleteCommandHandler : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                              _dateTime;
    private readonly ISerializer                            _serializer;
    private readonly IJsonWebToken                          _jsonWebToken;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public DeleteCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, 
        IDateTime dateTime, ISerializer serializer, IJsonWebToken jsonWebToken
    )
    {
        _dateTime                              = dateTime;
        _serializer                            = serializer;
        _jsonWebToken                          = jsonWebToken;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    public Task BeforeHandleAsync(DeleteCommand command, CancellationToken cancellationToken) => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(DeleteCommand command, CancellationToken cancellationToken)
    {
        var targetAnswer = _validationResult as ArticleCommentAnswer;
        var updatedBy    = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole  = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        targetAnswer.Delete(_dateTime, updatedBy, updatedRole);
        
        _articleCommentAnswerCommandRepository.Change(targetAnswer);

        return Task.FromResult(targetAnswer.Id);
    }

    public Task AfterHandleAsync(DeleteCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}