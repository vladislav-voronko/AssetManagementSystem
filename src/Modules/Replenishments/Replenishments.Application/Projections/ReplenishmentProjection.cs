using Replenishments.Contracts.Responses;
using Replenishments.Domain.Entities;
using System.Linq.Expressions;

namespace Replenishments.Application.Projections
{
    public static class ReplenishmentProjection
    {
        public static Expression<Func<Replenishment, ReplenishmentResponse>> ToResponse =>
            replenishment => new ReplenishmentResponse
            {
                Id = replenishment.Id,
                Amount = replenishment.Amount,
                Currency = replenishment.Currency,
                ReplenishedAt = replenishment.ReplenishedAt,
                Note = replenishment.Note,
                CreatedAt = replenishment.CreatedAt
            };
    }
}
