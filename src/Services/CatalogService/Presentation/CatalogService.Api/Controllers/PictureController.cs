using CatalogService.Application.Features.Images.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CatalogService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok("Catalog Service API is running");

        [HttpGet]
        [Route("api/v1/items/{itemId:int}/pic")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // GET: /<controller>/
        public async Task<ActionResult> GetImageAsync(int itemId)
        {
            GetImageQueryResponse response = await mediator.Send(new GetImageQueryRequest
            {
                ItemId = itemId
            });

            return File(response.Buffer, response.MimeType);
        }
    }
}
