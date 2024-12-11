using HTNest.Data.Interfaces;
using HTNest.Data.ViewModels;
using HTNest.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public Task<CartViewModel> AddToCart(string userName, int productId)
        {
            return _cartRepository.AddToCart(userName, productId);
        }

        public CartSummaryViewModel GetCart(string userId)
        {
            return _cartRepository.GetCart(userId);

        }

        public void RemoveAllCart(string userName)
        {
             _cartRepository.RemoveAllCart(userName);
        }

        public void RemoveItemsInCart(string userName, int productId)
        {
            _cartRepository.RemoveItemsInCart(userName, productId);
        }
    }
}
