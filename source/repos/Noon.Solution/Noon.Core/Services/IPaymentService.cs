using Noon.Core.Entities;

namespace Noon.Core.Services
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {

        }
    }
}
