#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;
using Domic.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Delete;

public class DeleteCommandHandler : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                              _dateTime;
    private readonly IJsonWebToken                          _jsonWebToken;
    private readonly ISerializer                            _serializer;
    private readonly IArticleCommentCommandRepository       _articleCommentCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public DeleteCommandHandler(IArticleCommentCommandRepository articleCommentCommandRepository,
        IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, IDateTime dateTime,
        IJsonWebToken jsonWebToken, ISerializer serializer
    )
    {
        _dateTime                              = dateTime;
        _jsonWebToken                          = jsonWebToken;
        _serializer                            = serializer;
        _articleCommentCommandRepository       = articleCommentCommandRepository;
        _articleCommentAnswerCommandRepository = articleCommentAnswerCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(ArticleCommentAnswerUseCase.Commands.Delete.DeleteCommand command, 
        CancellationToken cancellationToken
    )
    {
        var targetComment = _validationResult as ArticleComment;
        var updatedBy     = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole   = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        targetComment.Delete(_dateTime, updatedBy, updatedRole);

        await _articleCommentCommandRepository.ChangeAsync(targetComment, cancellationToken);

        foreach (var answer in targetComment.Answers)
        {
            answer.Delete(_dateTime, updatedBy, updatedRole, false);
            
            await _articleCommentAnswerCommandRepository.ChangeAsync(answer, cancellationToken);
        }

        return targetComment.Id;
    }
}