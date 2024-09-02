using CatalogService.Api.Models;
using CatalogService.Application.DTOs;
using CatalogService.Application.Features.Brands.Queries.GetAll;
using CatalogService.Application.Features.Items.Commands.Create;
using CatalogService.Application.Features.Items.Commands.Delete;
using CatalogService.Application.Features.Items.Commands.Update;
using CatalogService.Application.Features.Items.Queries.GetAllByPaging;
using CatalogService.Application.Features.Items.Queries.GetByBrandId;
using CatalogService.Application.Features.Items.Queries.GetById;
using CatalogService.Application.Features.Items.Queries.GetByIds;
using CatalogService.Application.Features.Items.Queries.GetByTypeIdAndBrandId;
using CatalogService.Application.Features.Items.Queries.GetWithName;
using CatalogService.Application.Features.Types.Queries.GetAll;
using CatalogService.Application.Interfaces.AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CatalogService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        private readonly IMediator mediator = mediator;
        private readonly IMapper mapper = mapper;

        // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(PaginatedItems<ItemDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<ItemDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 1, string? ids = null)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                IList<GetByIdsItemsQueryResponse> items = await mediator.Send(new GetByIdsItemsQueryRequest
                {
                    Ids = ids,
                });

                if (!items.Any())
                {
                    return BadRequest("ids value invalid. Must be comma-separated list of numbers");
                }

                return Ok(items);
            }

            GetAllByPagingItemsQueryResponse response = await mediator.Send(new GetAllByPagingItemsQueryRequest
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
            });
            return Ok(new PaginatedItems<ItemDTO>(pageIndex, pageSize, response.Count, response.Items));
        }

        [HttpGet]
        [Route("items/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ItemDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetByIdItemQueryResponse>> ItemByIdAsync(int id) => await mediator.Send(new GetByIdItemQueryRequest
        {
            Id = id
        });

        // GET api/v1/[controller]/items/withname/samplename[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/withname/{name:minlength(1)}")]
        [ProducesResponseType(typeof(PaginatedItems<ItemDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItems<ItemDTO>>> ItemsWithNameAsync(string name, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 1)
        {
            GetWithNameItemQueryResponse response = await mediator.Send(new GetWithNameItemQueryRequest
            {
                Name = name,
                PageSize = pageSize,
                PageIndex = pageIndex
            });

            return new PaginatedItems<ItemDTO>(pageIndex, pageSize, response.Count, response.Items);
        }

        // GET api/v1/[controller]/items/type/1/brand[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/type/{typeId}/brand/{brandId:int}")]
        [ProducesResponseType(typeof(PaginatedItems<ItemDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItems<ItemDTO>>> ItemsByTypeIdAndBrandIdAsync(int typeId, int brandId = 0, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 1)
        {
            GetByTypeIdAndBrandIdItemQueryResponse response = await mediator.Send(new GetByTypeIdAndBrandIdItemQueryRequest
            {
                TypeId = typeId,
                BrandId = brandId,
                PageSize = pageSize,
                PageIndex = pageIndex
            });
            return new PaginatedItems<ItemDTO>(pageIndex, pageSize, response.Count, response.Items);
        }

        // GET api/v1/[controller]/items/type/all/brand[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/type/all/brand/{brandId:int}")]
        [ProducesResponseType(typeof(PaginatedItems<ItemDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItems<ItemDTO>>> ItemsByBrandIdAsync(int brandId = 0, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 1)
        {
            GetByBrandIdItemsQueryResponse response = await mediator.Send(new GetByBrandIdItemsQueryRequest
            {
                BrandId = brandId,
                PageSize = pageSize,
                PageIndex = pageIndex
            });
            return new PaginatedItems<ItemDTO>(pageIndex, pageSize, response.Count, response.Items);
        }

        // GET api/v1/[controller]/Types
        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(List<GetAllTypesQueryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GetAllTypesQueryResponse>>> TypesAsync() => Ok(await mediator.Send(new GetAllTypesQueryRequest()));

        // GET api/v1/[controller]/Brands
        [HttpGet]
        [Route("brands")]
        [ProducesResponseType(typeof(List<GetAllBrandsQueryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<GetAllBrandsQueryResponse>>> BrandsAsync() => Ok(await mediator.Send(new GetAllBrandsQueryRequest()));

        //PUT api/v1/[controller]/items
        [Route("items")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateItemAsync([FromBody] ItemDTO itemToUpdate)
        {
            await mediator.Send(mapper.Map<UpdateItemCommandRequest, ItemDTO>(itemToUpdate));

            return CreatedAtAction(null, null);
        }

        //POST api/v1/[controller]/items
        [Route("items")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<ActionResult> CreateItemAsync([FromBody] ItemDTO item)
        {
            await mediator.Send(mapper.Map<CreateItemCommandRequest, ItemDTO>(item));

            return CreatedAtAction(null, null);
        }

        //DELETE api/v1/[controller]/id
        [Route("{id}/{deletedBy}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteItemAsync(int id, string deletedBy)
        {
            await mediator.Send(new DeleteItemCommandRequest
            {
                Id = id,
                DeletedBy = deletedBy
            });

            return NoContent();
        }
    }
}
