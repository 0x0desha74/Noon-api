using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noon.Core.Entities;
using Noon.Core.Repositories;

namespace Noon.API.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductsController(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepo.GetAllAsync();
        return Ok(products);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if(product is not null)
            {
            return Ok(product);
            }
            return Ok();
        }

    }
}
