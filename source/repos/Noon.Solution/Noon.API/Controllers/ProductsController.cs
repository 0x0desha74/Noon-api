using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noon.API.DTOs;
using Noon.API.Errors;
using Noon.Core.Entities;
using Noon.Core.Repositories;
using Noon.Core.Specifications;

namespace Noon.API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper, IGenericRepository<ProductBrand> brandsRepo, IGenericRepository<ProductType> typesRepo)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _brandsRepo = brandsRepo;
            _typesRepo = typesRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? sort,int? brandId, int? typeId)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(sort, brandId, typeId);

            var products = await _productRepo.GetAllWithSpec(spec);
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
        }



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {

            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _productRepo.GetByIdWithSpec(spec);


            if (product is null) return Ok();

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _typesRepo.GetAll();
            return Ok(types);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandsRepo.GetAll();
            return Ok(brands);
        }

    }
}
