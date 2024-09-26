using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Features.Order.Queries.GetById;

namespace OrderService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id) => Ok(await mediator.Send(new GetOrderByIdQueryRequest
        {
            OrderId = id
        }));
    }
}
