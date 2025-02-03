using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noon.API.DTOs;
using Noon.Core.Entities;
using Noon.Core.Repositories;
using Noon.Core.Specifications;

namespace Noon.API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var spec = new ProductWithBrandAndTypeSpecifications();

            var products = await _productRepo.GetAllWithSpec(spec);
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {

            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _productRepo.GetByIdWithSpec(spec);


            if (product is  null) return Ok();

            return Ok( _mapper.Map<Product,ProductToReturnDto>(product));
        }

    }
}
