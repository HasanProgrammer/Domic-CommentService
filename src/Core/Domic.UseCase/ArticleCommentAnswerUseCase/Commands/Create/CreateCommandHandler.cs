using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Create;

public class CreateCommandHandler : ICommandHandler<CreateCommand, string>
{
    private readonly IDateTime                              _dateTime;
    private readonly ISerializer                            _serializer;
    private readonly IJsonWebToken                          _jsonWebToken;
    private readonly IGlobalUniqueIdGenerator               _globalUniqueIdGenerator;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public CreateCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, 
        IDateTime dateTime, ISerializer serializer, IJsonWebToken jsonWebToken, 
        IGlobalUniqueIdGenerator globalUniqueIdGenerator
    )
    {
        _dateTime                              = dateTime;
        _serializer                            = serializer;
        _jsonWebToken                          = jsonWebToken;
        _globalUniqueIdGenerator               = globalUniqueIdGenerator;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var uniqueIdentity = _globalUniqueIdGenerator.GetRandom();
        var createdBy      = _jsonWebToken.GetIdentityUserId(command.Token);
        var createdRole    = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        var newAnswer = new ArticleCommentAnswer(
            _dateTime, uniqueIdentity, createdBy, createdRole, command.CommentId, command.Answer
        );

        await _articleCommentAnswerCommandRepository.AddAsync(newAnswer, cancellationToken);

        return newAnswer.Id;
    }
}