using Trades.Application.Interfaces;

namespace Trades.Application.Queries.Portfolio.GetPositions
{
    public class GetPositionsHandler
    {
        private readonly IPositionService _positionService;
        public GetPositionsHandler(IPositionService positionService)
        {
            _positionService = positionService;
        }

        public async Task<List<GetPositionsResponse>> Handle(GetPositionsQuery query, CancellationToken cancellationToken)
        {
            return await _positionService.GetPositionsAsync(cancellationToken);
        }
    }
}
