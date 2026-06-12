using BuildingBlocks.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Replenishments.Application.Commands.CreateReplenishment;
using Replenishments.Application.Queries.GetReplenishmentById;
using Replenishments.Application.Queries.GetReplenishments;
using Replenishments.Contracts.Requests;
using Replenishments.Contracts.Responses;

namespace AssetManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/replenishments")]
    public class ReplenishmentController : ControllerBase
    {
        private readonly CreateReplenishmentHandler _createReplenishmentHandler;
        private readonly GetReplenishmentByIdHandler _getReplenishmentByIdHandler;
        private readonly GetReplenishmentsHandler _getReplenishmentsHandler;

        public ReplenishmentController(CreateReplenishmentHandler createReplenishmentHandler,
            GetReplenishmentByIdHandler getReplenishmentByIdHandler,
            GetReplenishmentsHandler getReplenishmentsHandler)
        {
            _createReplenishmentHandler = createReplenishmentHandler;
            _getReplenishmentByIdHandler = getReplenishmentByIdHandler;
            _getReplenishmentsHandler = getReplenishmentsHandler;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<ReplenishmentResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery] GetReplenishmentsQuery query, CancellationToken cancellationToken)
        {
            var replenishments = await _getReplenishmentsHandler.Handle(query, cancellationToken);

            return replenishments is null
                ? NotFound()
                : Ok(replenishments);
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ReplenishmentResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var replenishment = await _getReplenishmentByIdHandler.Handle(new GetReplenishmentByIdQuery(id), cancellationToken);

            return replenishment is null
                ? NotFound()
                : Ok(replenishment);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReplenishmentResponse), 201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Create([FromBody] CreateReplenishmentRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateReplenishmentCommand
            {
                Amount = request.Amount,
                Currency = request.Currency,
                ReplenishedAt = request.ReplenishedAt,
                Note = request.Note
            };

            var replenishment = await _createReplenishmentHandler.Handle(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = replenishment.Id }, replenishment);
        }
    }
}
