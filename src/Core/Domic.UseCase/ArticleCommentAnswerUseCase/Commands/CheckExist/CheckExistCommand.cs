using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentAnswerUseCase.Commands.CheckExist;

public class CheckExistCommand : IQuery<bool>
{
    public required string Id { get; set; }
}