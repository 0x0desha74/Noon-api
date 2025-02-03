using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Noon.API.Errors;
using Noon.Repository.Data;

namespace Noon.API.Controllers
{

        public class BuggyController : BaseApiController
        {
            private readonly StoreContext _storeContext;

            public BuggyController(StoreContext storeContext)
            {
                _storeContext = storeContext;
            }


            [HttpGet("notfound")]
            public ActionResult GetNotFound()
            {
                return NotFound(new ApiResponse(404));
            }

            [HttpGet("servererror")]
            public ActionResult GetServerError()
            {
                var product = _storeContext.Products.Find(100);
                var ProductToReturn = product.ToString();
                return Ok(ProductToReturn);
            }
            [HttpGet("badrequest")]
            public ActionResult GetBadRequest()
            {
                return BadRequest(new ApiResponse(400));
            }

            [HttpGet("badrequest/{id}")]
            public ActionResult GetBadRequest(int id)
            {
                return Ok();
            }

        }
    }
