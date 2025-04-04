using Domic.UseCase.ArticleCommentUseCase.DTOs;
using Domic.Core.Common.ClassExtensions;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryHandler(IInternalDistributedCacheMediator distributedCacheMediator) : IQueryHandler<ReadAllPaginatedQuery, PaginatedCollection<ArticleCommentDto>> 
{
    [WithValidation]
    public async Task<PaginatedCollection<ArticleCommentDto>> HandleAsync(ReadAllPaginatedQuery query, 
        CancellationToken cancellationToken
    )
    {
        var result = await distributedCacheMediator.GetAsync<List<ArticleCommentDto>>(cancellationToken);

        return result.ToPaginatedCollection(result.Count, query.CountPerPage ?? 0, query.PageNumber ?? 0);
    }
}