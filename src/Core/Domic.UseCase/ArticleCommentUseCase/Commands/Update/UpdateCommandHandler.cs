#pragma warning disable CS0649

using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Contracts.Interfaces;
using Domic.Domain.ArticleComment.Entities;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Update;

public class UpdateCommandHandler : ICommandHandler<UpdateCommand, string>
{
    private readonly object _validationResult;

    private readonly IDateTime                        _dateTime;
    private readonly ISerializer                      _serializer;
    private readonly IJsonWebToken                    _jsonWebToken;
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;

    public UpdateCommandHandler(IArticleCommentCommandRepository articleCommentCommandRepository, 
        IDateTime dateTime, ISerializer serializer
    )
    {
        _dateTime                        = dateTime;
        _serializer                      = serializer;
        _articleCommentCommandRepository = articleCommentCommandRepository;
    }

    [WithValidation]
    [WithTransaction]
    [WithCleanCache(Keies = Cache.ArticleComments)]
    public async Task<string> HandleAsync(UpdateCommand command, CancellationToken cancellationToken)
    {
        var targetComment = _validationResult as ArticleComment;
        var updatedBy     = _jsonWebToken.GetIdentityUserId(command.Token);
        var updatedRole   = _serializer.Serialize( _jsonWebToken.GetRoles(command.Token) );
        
        targetComment.Change(_dateTime, updatedBy, updatedRole, command.Comment);

        await _articleCommentCommandRepository.ChangeAsync(targetComment, cancellationToken);

        return targetComment.Id;
    }
}