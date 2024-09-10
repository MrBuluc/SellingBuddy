using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiGateway.Application.Features.BasketItem.Command.Add;

namespace Web.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost]
        [Route("items")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddBasketItemAsync([FromBody] AddBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
            await mediator.Send(request, cancellationToken);

            return Ok();
        }
    }
}
