﻿using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Commands.Active;

public class ActiveCommand : ICommand<string>
{    public required string Id { get; set; }
}