﻿using BasketService.Application.Bases;
using BasketService.Application.DTOs;
using BasketService.Application.Interfaces.AutoMapper;
using BasketService.Application.Interfaces.UnitOfWorks;
using MediatR;

namespace BasketService.Application.Features.CustomerBasket.Command.Update
{
    public class UpdateCustomerBasketCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : BaseHandler(mapper, unitOfWork), IRequestHandler<UpdateCustomerBasketCommandRequest, UpdateCustomerBasketCommandResponse>
    {
        public async Task<UpdateCustomerBasketCommandResponse> Handle(UpdateCustomerBasketCommandRequest request, CancellationToken cancellationToken) => new UpdateCustomerBasketCommandResponse
        {
            CustomerBasket = mapper.Map<CustomerBasketDTO, Domain.Entities.CustomerBasket>(await unitOfWork.GetWriteRepository<Domain.Entities.CustomerBasket>().UpdateAsync(mapper.Map<Domain.Entities.CustomerBasket, CustomerBasketDTO>(request.CustomerBasket)))
        };
    }
}
