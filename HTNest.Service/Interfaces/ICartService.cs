using HTNest.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Service.Interfaces
{
    public interface ICartService
    {
        Task<CartViewModel> AddToCart(string userName, int productId);
        CartSummaryViewModel GetCart(string userId);
        void RemoveItemsInCart(string userName, int productId);
        void RemoveAllCart(string userName);
    }
}
