using OrderService.Application.Interfaces.AutoMapper;
using OrderService.Application.Interfaces.UnitOfWorks;

namespace OrderService.Application.Bases
{
    public class BaseHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        public readonly IMapper mapper = mapper;
        public readonly IUnitOfWork unitOfWork = unitOfWork;
    }
}
