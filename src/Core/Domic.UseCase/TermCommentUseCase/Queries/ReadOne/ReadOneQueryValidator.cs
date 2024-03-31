using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.TermComment.Contracts.Interfaces;

namespace Domic.UseCase.TermCommentUseCase.Queries.ReadOne;

public class ReadOneQueryValidator : IValidator<ReadOneQuery>
{
    private readonly ITermCommentCommandRepository _termCommentCommandRepository;

    public ReadOneQueryValidator(ITermCommentCommandRepository termCommentCommandRepository) 
        => _termCommentCommandRepository = termCommentCommandRepository;

    public async Task<object> ValidateAsync(ReadOneQuery input, CancellationToken cancellationToken)
    {
        var targetComment =
            await _termCommentCommandRepository.FindByIdEagerLoadingAsync(input.Id, cancellationToken);

        if (targetComment is null)
            throw new UseCaseException(
                string.Format("موجودیتی با شناسه {0} وجود خارجی ندارد !", input.Id ?? "_خالی_")
            );

        return targetComment;
    }
}