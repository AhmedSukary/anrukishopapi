using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Application.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICheckoutRepository _repo;

        public CheckoutService(ICheckoutRepository checkoutRepository)
        {
            _repo = checkoutRepository; 
        }

        public int Checkout(int userId)
        {
            try
            {
                return _repo.Checkout(userId);
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}