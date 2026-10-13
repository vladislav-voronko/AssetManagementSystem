using Assets.Domain.Enums;
using Shared.Application.Pagination;

namespace Assets.Application.Queries.GetAssets
{
    public sealed record GetAssetsQuery : PagedQuery
    {
        public string? Search { get; init; }
        public AssetType? Type { get; init; }
        public bool? IsActive { get; init; }
    }
}
