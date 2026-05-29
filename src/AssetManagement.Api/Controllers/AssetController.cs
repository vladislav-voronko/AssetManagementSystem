using Assets.Application.Commands.CreateAsset;
using Assets.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/assets")]
    public class AssetsController : ControllerBase
    {
        private readonly CreateAssetHandler _createAssetHandler;

        public AssetsController(CreateAssetHandler createAssetHandler)
        {
            _createAssetHandler = createAssetHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAssetRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateAssetCommand
            {
                Symbol = request.Symbol,
                Name = request.Name,
                Type = request.Type,
                Currency = request.Currency
            };

            var assetId = await _createAssetHandler.Handle(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = assetId }, assetId);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            return Ok();
        }
    }
}
