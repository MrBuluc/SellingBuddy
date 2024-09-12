using BasketService.Application.Interfaces.AutoMapper;
using BasketService.Application.Interfaces.UnitOfWorks;

namespace BasketService.Application.Bases
{
    public class BaseHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        public readonly IMapper mapper = mapper;
        public readonly IUnitOfWork unitOfWork = unitOfWork;
    }
}
