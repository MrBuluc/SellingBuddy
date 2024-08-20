using CatalogService.Application.Interfaces.AutoMapper;
using CatalogService.Application.Interfaces.UnitOfWorks;

namespace CatalogService.Application.Bases
{
    public class BaseHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        public readonly IMapper mapper = mapper;
        public readonly IUnitOfWork unitOfWork = unitOfWork;
    }
}
