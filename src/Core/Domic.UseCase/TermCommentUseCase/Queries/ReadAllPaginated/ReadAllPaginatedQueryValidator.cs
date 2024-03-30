using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;

namespace Domic.UseCase.ArticleCommentUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryValidator : IValidator<ReadAllPaginatedQuery>
{
    public async Task<object> ValidateAsync(ReadAllPaginatedQuery input, CancellationToken cancellationToken)
    {
        await Task.Run(() => {

            if(input.PageNumber is null)
                throw new UseCaseException("فیلد شماره صفحه الزامی می باشد !");
            
            if(input.CountPerPage is null)
                throw new UseCaseException("فیلد تعداد آیتم هر صفحه الزامی می باشد !");
            
        }, cancellationToken);

        return default;
    }
}