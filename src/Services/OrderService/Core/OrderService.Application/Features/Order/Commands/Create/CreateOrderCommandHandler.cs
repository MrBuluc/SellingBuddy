using EventBus.Base.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.Application.Bases;
using OrderService.Application.DTOs;
using OrderService.Application.IntegrationEvents;
using OrderService.Application.Interfaces.AutoMapper;
using OrderService.Application.Interfaces.UnitOfWorks;
using OrderService.Domain.AggregateModels.OrderAggregate;

namespace OrderService.Application.Features.Order.Commands.Create
{
    public class CreateOrderCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus, ILogger<CreateOrderCommandHandler> logger, IMapper mapper) : BaseHandler(mapper, unitOfWork), IRequestHandler<CreateOrderCommandRequest, bool>
    {
        private readonly IEventBus eventBus = eventBus;
        private readonly ILogger<CreateOrderCommandHandler> logger = logger;

        public async Task<bool> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateOrderCommandHandler -> Handle method invoked");

            Domain.AggregateModels.OrderAggregate.Order dbOrder = mapper.Map<Domain.AggregateModels.OrderAggregate.Order, CreateOrderCommandRequest>(request);

            foreach (OrderItemDTO orderItem in request.OrderItems)
            {
                dbOrder.AddOrderItem(mapper.Map<Product, ProductDTO>(orderItem.Product), orderItem.Units);
            }

            await unitOfWork.GetWriteRepository<Domain.AggregateModels.OrderAggregate.Order>().AddAsync(dbOrder);
            await unitOfWork.SaveEntitiesAsync(cancellationToken);

            logger.LogInformation("CreateOrderCommandHandler -> dbOrder saved");

            eventBus.Publish(new OrderStartedIntegrationEvent
            {
                UserName = request.UserName,
                OrderId = dbOrder.Id
            });

            logger.LogInformation("CreateOrderCommandHandler -> OrderStartedIntegrationEvent fired");

            return true;
        }
    }
}
