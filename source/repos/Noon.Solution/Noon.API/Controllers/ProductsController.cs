using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noon.API.DTOs;
using Noon.API.Errors;
using Noon.API.Helpers;
using Noon.Core;
using Noon.Core.Entities;
using Noon.Core.Repositories;
using Noon.Core.Specifications;

namespace Noon.API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(specParams);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(spec);
            var countSpec = new ProductWithFilterationForCountSpecification(specParams);
            var Count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(countSpec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageSize, specParams.PageIndex, Count, data));
        
        
        }




        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {

            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec);


            if (product is null) return Ok(new ApiResponse(404));

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }



        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAll();
            return Ok(types);
        }


        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAll();
            return Ok(brands);
        }

    }
}
