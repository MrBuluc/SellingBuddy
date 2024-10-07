using BasketService.Api.IntegrationEvents.Events;
using BasketService.Application.Features.CustomerBasket.Command.Delete;
using EventBus.Base.Abstraction;
using MediatR;

namespace BasketService.Api.IntegrationEvents.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler(IMediator mediator, ILogger<OrderCreatedIntegrationEvent> logger) : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IMediator mediator = mediator;
        private readonly ILogger<OrderCreatedIntegrationEvent> logger = logger;

        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            logger.LogInformation("----- Handling integration event: {IntegrationEventId} at BasketService.Api - ({@IntegrationEvent})", @event.Id, @event);

            await mediator.Send(new DeleteCustomerBasketCommandRequest
            {
                BuyerId = @event.User.Id
            });
        }
    }
}
