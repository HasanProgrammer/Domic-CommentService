using Domic.UseCase.ArticleCommentUseCase.DTOs.ViewModels;
using Domic.Core.Common.ClassExtensions;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.ArticleCommentUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryHandler : IQueryHandler<ReadAllPaginatedQuery, PaginatedCollection<ArticleCommentsViewModel>> 
{
    private readonly ICacheService _cacheService;

    public ReadAllPaginatedQueryHandler(ICacheService cacheService) => _cacheService = cacheService;

    [WithValidation]
    public async Task<PaginatedCollection<ArticleCommentsViewModel>> HandleAsync(ReadAllPaginatedQuery query, 
        CancellationToken cancellationToken
    )
    {
        var result = await _cacheService.GetAsync<List<ArticleCommentsViewModel>>(cancellationToken);

        return result.ToPaginatedCollection(result.Count, query.CountPerPage ?? 0, query.PageNumber ?? 0);
    }
}