using Domic.Core.Common.ClassExtensions;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.UseCase.TermCommentUseCase.DTOs;

namespace Domic.UseCase.TermCommentUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryHandler : IQueryHandler<ReadAllPaginatedQuery, PaginatedCollection<TermCommentsDto>>
{
    private readonly ICacheService _cacheService;

    public ReadAllPaginatedQueryHandler(ICacheService cacheService) => _cacheService = cacheService;

    [WithValidation]
    public async Task<PaginatedCollection<TermCommentsDto>> HandleAsync(ReadAllPaginatedQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = await _cacheService.GetAsync<List<TermCommentsDto>>(cancellationToken);

        return result.ToPaginatedCollection(result.Count, query.CountPerPage ?? 0, query.PageNumber ?? 0);
    }
}