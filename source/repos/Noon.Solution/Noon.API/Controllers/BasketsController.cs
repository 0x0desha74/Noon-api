using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noon.API.Errors;
using Noon.Core.Entities;
using Noon.Core.Repositories;

namespace Noon.API.Controllers
{

    public class BasketsController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }


        //used to get basket or recreate expired ones with the same baskedId
        [HttpGet] //GET : api/baskets?id=
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket ?? new CustomerBasket(id);
        }



        [HttpPost] //POST : api/baskets
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);

            if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
            return CreatedOrUpdatedBasket;

        }
                       
        [HttpDelete] //DELETE : api/baskets?id=
        public async Task<bool> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
