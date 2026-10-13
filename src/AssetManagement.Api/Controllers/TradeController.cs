using Shared.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trades.Application.Commands.CreateTrade;
using Trades.Application.Queries.GetTradeById;
using Trades.Application.Queries.GetTrades;
using Trades.Contracts.Requests;
using Trades.Contracts.Responses;

namespace AssetManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/trades")]
    public class TradeController : ControllerBase
    {
        private readonly CreateTradeHandler _createTradeHandler;
        private readonly GetTradeByIdHandler _getTradeByIdHandler;
        private readonly GetTradesHandler _getTradesHandler;  
        public TradeController(CreateTradeHandler createTradeHandler, 
            GetTradeByIdHandler getTradeByIdHandler,
            GetTradesHandler getTradesHandler)
        {
            _createTradeHandler = createTradeHandler;
            _getTradeByIdHandler = getTradeByIdHandler;
            _getTradesHandler = getTradesHandler;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<TradeResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery] GetTradesQuery query, CancellationToken cancellationToken)
        {
            var trades = await _getTradesHandler.Handle(query, cancellationToken);

            return trades is null
                ? NotFound()
                : Ok(trades);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TradeResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var trade = await _getTradeByIdHandler.Handle(new GetTradeByIdQuery(id), cancellationToken);

            return trade is null
                ? NotFound()
                : Ok(trade);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TradeResponse), 201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Create([FromBody]CreateTradeRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateTradeCommand
            {
                AssetId = request.AssetId,
                Type = request.Type,
                Quantity = request.Quantity,
                Price = request.Price,
                Currency = request.Currency,
                ExecutedAt = request.ExecutedAt
            };

            var trade = await _createTradeHandler.Handle(command, cancellationToken);
            
            return CreatedAtAction(nameof(GetById), new { id = trade.Id }, trade);
        }
    }
}
