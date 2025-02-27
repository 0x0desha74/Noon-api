﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noon.API.DTOs;
using Noon.API.Errors;
using Noon.Core.Entities;
using Noon.Core.Repositories;

namespace Noon.API.Controllers
{

    public class BasketsController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        //used to get basket or recreate expired ones with the same baskedId
        [HttpGet] //GET : api/baskets?id=
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket ?? new CustomerBasket(id);
        }


        //used to update or create new basket
        [HttpPost] //POST : api/baskets
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);

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
