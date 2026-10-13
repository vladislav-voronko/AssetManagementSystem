using Assets.Application.Commands.CreateAsset;
using Assets.Application.Commands.SwitchAssetStatus;
using Assets.Application.Commands.UpdateAsset;
using Assets.Application.Queries.GetAssetById;
using Assets.Application.Queries.GetAssets;
using Assets.Contracts.Requests;
using Assets.Contracts.Responses;
using Shared.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly SwitchAssetStatusHandler _switchAssetStatusHandler;


        public AssetController(GetAssetsHandler getAssetsHandler,
            GetAssetByIdHandler getAssetByIdHandler,
            CreateAssetHandler createAssetHandler,
            UpdateAssetHandler updateAssetHandler,
            SwitchAssetStatusHandler switchAssetStatusHandler)
        {
            _getAssetsHandler = getAssetsHandler;
            _getAssetByIdHandler = getAssetByIdHandler;
            _createAssetHandler = createAssetHandler;
            _updateAssetHandler = updateAssetHandler;
            _switchAssetStatusHandler = switchAssetStatusHandler;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<AssetResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromQuery] GetAssetsQuery query, CancellationToken cancellationToken)
        {
            var assets = await _getAssetsHandler.Handle(query, cancellationToken);
            return assets is null
                ? NotFound()
                : Ok(assets);
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
            };

            var asset = await _updateAssetHandler.Handle(command, cancellationToken);

            return asset is null
                ? NotFound()
                : Ok(asset);
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType(typeof(AssetResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SwitchStatus([FromRoute] Guid id, [FromBody] bool assetStatus, CancellationToken cancellationToken)
        {
            var command = new SwitchAssetStatusCommand { Id = id, IsActive = assetStatus };

            var asset = await _switchAssetStatusHandler.Handle(command, cancellationToken);

            return asset is null
                ? NotFound()
                : Ok(asset);
        }
    }
}
