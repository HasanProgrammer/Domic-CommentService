#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;
using Domic.Domain.ArticleCommentAnswer.Entities;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.Update;

public class UpdateCommandHandler : ICommandHandler<UpdateCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                              _dateTime;
    private readonly ISerializer                            _serializer;
    private readonly IJsonWebToken                          _jsonWebToken;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public UpdateCommandHandler(IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, 
        IDateTime dateTime, ISerializer serializer, IJsonWebToken jsonWebToken
    )
    {
        _dateTime                              = dateTime;
        _serializer                            = serializer;
        _jsonWebToken                          = jsonWebToken;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public Task<string> HandleAsync(UpdateCommand command, CancellationToken cancellationToken)
    {
        var answer      = _validationResult as ArticleCommentAnswer;
        var updatedBy   = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        answer.Change(_dateTime, updatedBy, updatedRole, command.Answer);

        _articleCommentAnswerCommandRepository.Change(answer);

        return Task.FromResult(answer.Id);
    }
}