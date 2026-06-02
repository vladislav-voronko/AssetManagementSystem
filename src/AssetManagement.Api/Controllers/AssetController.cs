using Assets.Application.Commands.CreateAsset;
using Assets.Application.Commands.DeleteAsset;
using Assets.Application.Commands.UpdateAsset;
using Assets.Application.Queries.GetAssetById;
using Assets.Application.Queries.GetAssets;
using Assets.Contracts.Requests;
using Assets.Contracts.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trades.Contracts.Responses;

namespace AssetManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/assets")]
    public class AssetController : ControllerBase
    {
        private readonly GetAssetsHandler _getAssetsHandler;
        private readonly GetAssetByIdHandler _getAssetByIdHandler;
        private readonly CreateAssetHandler _createAssetHandler;
        private readonly UpdateAssetHandler _updateAssetHandler;
        private readonly DeleteAssetHandler _deleteAssetHandler;


        public AssetController(GetAssetsHandler getAssetsHandler,
            GetAssetByIdHandler getAssetByIdHandler,
            CreateAssetHandler createAssetHandler,
            UpdateAssetHandler updateAssetHandler,
            DeleteAssetHandler deleteAssetHandler)
        {
            _getAssetsHandler = getAssetsHandler;
            _getAssetByIdHandler = getAssetByIdHandler;
            _createAssetHandler = createAssetHandler;
            _updateAssetHandler = updateAssetHandler;
            _deleteAssetHandler = deleteAssetHandler;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AssetResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var assets = await _getAssetsHandler.Handle(new GetAssetsQuery(), cancellationToken);
            return Ok(assets);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AssetResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var asset = await _getAssetByIdHandler.Handle(new GetAssetByIdQuery(id), cancellationToken);

            return asset is null
                ? NotFound()
                : Ok(asset);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AssetResponse), 201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Create([FromBody] CreateAssetRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateAssetCommand
            {
                Symbol = request.Symbol,
                Name = request.Name,
                Type = request.Type,
                Currency = request.Currency
            };

            var asset = await _createAssetHandler.Handle(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = asset.Id }, asset);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(AssetResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAssetRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateAssetCommand
            {
                Id = id,
                Symbol = request.Symbol,
                Name = request.Name,
                Type = request.Type,
                Currency = request.Currency
            };

            var result = await _updateAssetHandler.Handle(command, cancellationToken);

            return result is null
                ? NotFound()
                : Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteAssetCommand { Id = id };

            var result = await _deleteAssetHandler.Handle(command, cancellationToken);

            return result
                ? NoContent()
                : NotFound();
        }
    }
}
