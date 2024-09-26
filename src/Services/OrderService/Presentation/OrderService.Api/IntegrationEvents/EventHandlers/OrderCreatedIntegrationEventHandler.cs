using EventBus.Base.Abstraction;
using MediatR;
using OrderService.Api.IntegrationEvents.Events;
using OrderService.Application.DTOs;
using OrderService.Application.Features.Order.Commands.Create;
using OrderService.Application.Interfaces.AutoMapper;
using OrderService.Domain.AggregateModels.OrderAggregate;

namespace OrderService.Api.IntegrationEvents.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler(IMediator mediator, ILogger<OrderCreatedIntegrationEventHandler> logger, IMapper mapper) : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IMediator mediator = mediator;
        private readonly ILogger<OrderCreatedIntegrationEventHandler> logger = logger;
        private readonly IMapper mapper = mapper;

        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            try
            {
                logger.LogInformation("Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, typeof(Startup).Namespace, @event);

                mapper.Map<ProductDTO, Product>(new Product());

                await mediator.Send(new CreateOrderCommandRequest
                {
                    UserName = @event.UserName,
                    Address = @event.Address,
                    Card = @event.Card,
                    OrderItems = mapper.Map<OrderItemDTO, Item>(@event.Basket.Items)
                });
            } catch (Exception ex)
            {
                logger.LogError(ex, "OrderCreatedIntegrationEventHandler Error");
            }
        }
    }
}
