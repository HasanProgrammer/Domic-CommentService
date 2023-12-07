using Karami.Core.UseCase.Contracts.Interfaces;

namespace Karami.UseCase.ArticleCommentAnswerUseCase.Commands.CheckExist;

public class CheckExistCommand : IQuery<bool>
{
    public required string AnswerId { get; set; }
}