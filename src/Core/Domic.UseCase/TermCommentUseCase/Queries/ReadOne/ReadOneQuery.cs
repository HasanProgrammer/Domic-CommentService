using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.UseCase.TermCommentUseCase.DTOs;

namespace Domic.UseCase.TermCommentUseCase.Queries.ReadOne;

public class ReadOneQuery : IQuery<TermCommentsDto>
{
    public string Id { get; set; }
}