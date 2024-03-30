﻿using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.ArticleComment.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Update;

public class UpdateCommandValidator : IValidator<UpdateCommand>
{
    private readonly IArticleCommentCommandRepository _articleCommentCommandRepository;

    public UpdateCommandValidator(IArticleCommentCommandRepository articleCommentCommandRepository)
    {
        _articleCommentCommandRepository = articleCommentCommandRepository;
    }

    public async Task<object> ValidateAsync(UpdateCommand input, CancellationToken cancellationToken)
    {
        var targetComment = await _articleCommentCommandRepository.FindByIdAsync(input.TargetId, cancellationToken);
        
        if (targetComment is null)
            throw new UseCaseException(
                string.Format("فیلدی با شناسه {0} وجود خارجی ندارد !", input.TargetId ?? "_خالی_")
            );

        return targetComment;
    }
}