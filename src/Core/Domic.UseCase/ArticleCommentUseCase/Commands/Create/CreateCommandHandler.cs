using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Create;

public class CreateCommandHandler : ICommandHandler<CreateCommand, string>
{
    private readonly IDateTime                        _dateTime;
    private readonly ISerializer                      _serializer;
    private readonly IJsonWebToken                    _jsonWebToken;
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;
    private readonly IGlobalUniqueIdGenerator         _globalUniqueIdGenerator;

    public CreateCommandHandler(IArticleCommentCommandRepository articleCommentCommandRepository, 
        IDateTime dateTime, ISerializer serializer, IJsonWebToken jsonWebToken, 
        IGlobalUniqueIdGenerator globalUniqueIdGenerator
    )
    {
        _dateTime                        = dateTime;
        _serializer                      = serializer;
        _jsonWebToken                    = jsonWebToken;
        _articleCommentCommandRepository = articleCommentCommandRepository;
        _globalUniqueIdGenerator         = globalUniqueIdGenerator;
    }

    [WithTransaction]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var uniqueIdentity = _globalUniqueIdGenerator.GetRandom();
        var createdBy      = _jsonWebToken.GetIdentityUserId(command.Token);
        var createdRole    = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        var newComment = new ArticleComment(
            _dateTime, uniqueIdentity, createdBy, command.ArticleId, createdRole, command.Comment
        );

        await _articleCommentCommandRepository.AddAsync(newComment, cancellationToken);

        return newComment.Id;
    }

    public Task AfterTransactionHandleAsync(CreateCommand message, CancellationToken cancellationToken)
        => Task.CompletedTask;
}