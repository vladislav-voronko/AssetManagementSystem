using Replenishments.Contracts.Responses;
using Replenishments.Domain.Entities;

namespace Replenishments.Application.Mappings
{
    public static class ReplenishmentMapping
    {
        public static ReplenishmentResponse ToResponse(this Replenishment replenishment) =>
            new ReplenishmentResponse
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
