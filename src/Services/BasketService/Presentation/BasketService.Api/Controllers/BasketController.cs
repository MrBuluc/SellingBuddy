using BasketService.Api.IntegrationEvents.Events;
using BasketService.Application.DTOs;
using BasketService.Application.Features.CustomerBasket.Command.AddItem;
using BasketService.Application.Features.CustomerBasket.Command.Checkout;
using BasketService.Application.Features.CustomerBasket.Command.Delete;
using BasketService.Application.Features.CustomerBasket.Command.Update;
using BasketService.Application.Features.CustomerBasket.Queries.GetById;
using EventBus.Base.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace BasketService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController(IMediator mediator, IEventBus eventBus, ILogger<BasketController> logger) : ControllerBase
    {
        private readonly IMediator mediator = mediator;
        private readonly IEventBus eventBus = eventBus;
        private readonly ILogger<BasketController> logger = logger;

        [HttpGet]
        public IActionResult Get() => Ok("Basket Service is Up and Running");

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasketDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasketDTO>> GetBasketByIdAsync(string id, CancellationToken cancellationToken) => Ok((await mediator.Send(new GetCustomerBasketByIdQueryRequest
        {
            Id = id,
        }, cancellationToken))
            .CustomerBasket);

        [HttpGet("mybasket")]
        [ProducesResponseType(typeof(CustomerBasketDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasketDTO>> GetMyBasketAsync(CancellationToken cancellationToken) => Ok((await mediator.Send(new GetCustomerBasketByIdQueryRequest
        {
            Id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        }, cancellationToken))
            .CustomerBasket);

        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(CustomerBasketDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasketDTO>> UpdateBasketAsync([FromBody] CustomerBasketDTO customerBasketDTO, CancellationToken cancellationToken) => Ok((await mediator.Send(new UpdateCustomerBasketCommandRequest
        {
            CustomerBasket = customerBasketDTO
        }, cancellationToken))
            .CustomerBasket);

        [Route("additem")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<ActionResult> AddItemToBasketAsync([FromBody] AddItemCustomerBasketCommandRequest request, CancellationToken cancellationToken)
        {
            request.Id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await mediator.Send(request, cancellationToken);

            return Ok();
        }

        [Route("checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync([FromBody] CheckoutCustomerBasketCommandRequest request, CancellationToken cancellationToken)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            OrderCreatedIntegrationEvent eventMessage = new()
            {
                User = new()
                {
                    Id = userId,
                    Name = User.FindFirst(JwtRegisteredClaimNames.Name)!.Value,
                    Email = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value
                },
                OrderNumber = Guid.NewGuid(),
                Address = request.Address,
                Card = request.Card,
                Buyer = request.Buyer,
                Basket = (await mediator.Send(new GetCustomerBasketByIdQueryRequest
                {
                    Id = userId
                }, cancellationToken)).CustomerBasket
            };

            try
            {
                // listen itself to clean the basket
                // it is listened by OrderApi to start the process
                eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {BasketService.Api}", eventMessage.Id);

                throw;
            }

            return Accepted();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteBasketByIdAsync(string id)
        {
            await mediator.Send(new DeleteCustomerBasketCommandRequest
            {
                BuyerId = id
            });
        }
    }
}
