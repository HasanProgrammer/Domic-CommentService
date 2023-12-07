#pragma warning disable CS0649

using Karami.Core.Domain.Contracts.Interfaces;
using Karami.Core.UseCase.Attributes;
using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Contracts.Interfaces;
using Karami.Domain.ArticleComment.Entities;
using Karami.Domain.ArticleCommentAnswer.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentUseCase.Commands.Delete;

public class DeleteCommandHandler : ICommandHandler<DeleteCommand, string>
{
    private readonly object _validationResult;

    private readonly IDotrisDateTime                        _dotrisDateTime;
    private readonly IArticleCommentCommandRepository       _articleCommentCommandRepository;
    private readonly IArticleCommentAnswerCommandRepository _articleCommentAnswerCommandRepository;

    public DeleteCommandHandler(IArticleCommentCommandRepository articleCommentCommandRepository,
        IArticleCommentAnswerCommandRepository articleCommentAnswerCommandRepository, IDotrisDateTime dotrisDateTime
    )
    {
        _dotrisDateTime                        = dotrisDateTime;
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
        
        targetComment.Delete(_dotrisDateTime);

        await _articleCommentCommandRepository.ChangeAsync(targetComment, cancellationToken);

        foreach (var answer in targetComment.Answers)
        {
            answer.Delete(_dotrisDateTime, false);
            
            await _articleCommentAnswerCommandRepository.ChangeAsync(answer, cancellationToken);
        }

        return targetComment.Id;
    }
}