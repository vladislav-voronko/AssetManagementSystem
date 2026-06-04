using Trades.Application.Queries.Portfolio.GetPositions;

namespace Trades.Application.Interfaces
{
    public interface IPositionService
    {
        public Task<decimal> GetPositionAsync(Guid assetId, CancellationToken cancellationToken);
        public Task<List<GetPositionsResponse>> GetPositionsAsync(CancellationToken cancellationToken);
    }
}