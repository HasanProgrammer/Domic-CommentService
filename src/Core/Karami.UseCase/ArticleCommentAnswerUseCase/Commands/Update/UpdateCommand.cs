﻿using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Commands.Update;

public class UpdateCommand : ICommand<string>
{
    public required string Token    { get; init; }
    public required string TargetId { get; init; }
    public required string Answer   { get; init; }
}