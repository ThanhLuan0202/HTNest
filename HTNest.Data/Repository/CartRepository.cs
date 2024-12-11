using HTNest.Data.Interfaces;
using HTNest.Data.Model.Cart;
using HTNest.Data.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTNest.Data.Repository
{
    public class CartRepository : ICartRepository
    {
        private static readonly ConcurrentDictionary<string, List<CartItem>> Carts = new();
        private readonly IProductRepository _productRepository;
        public CartRepository(IProductRepository productRepository)
        {
             _productRepository = productRepository;
        }


        public async Task<CartViewModel> AddToCart(string userName, int productId)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("User ID cannot be null or empty");

            }
            if (!Carts.ContainsKey(userName))
            {
                Carts[userName] = new List<CartItem>();
            }
            var cart = Carts[userName];
            var existingItem = cart.Find(x => x.ProductId == productId);
            var product = await _productRepository.GetById(productId);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with ID {productId} not found.");

            }

            var cartViewModel = new CartViewModel
            {
                ProductId = product.ProductId,
                ProductCode = product.ProductCode,
                Description = product.Description,
                Price = product.Price,
                AddedDate = DateTime.Now.AddHours(7),
                Discount = (double)product.Discount,

            };
            if (existingItem == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    AddedDate = DateTime.Now.AddHours(7),
                });
                cartViewModel.Message = $"Product '{product.ProductId}' added to cart successfully.";

            }
            else
            {
                cartViewModel.Message = $"Product '{product.ProductId}' is already in the cart.";

            }
            return cartViewModel;
        }


        public CartSummaryViewModel GetCart(string userName)
        {
            var cartSummary = new CartSummaryViewModel();
            if (Carts.ContainsKey(userName))
            {
                var cartItems = Carts[userName];
                int totalItem = 0;
                double totalPrice = 0;

                foreach (var item in cartItems)
                {
                    var product = _productRepository.GetById(item.ProductId).Result;
                    if (product != null)
                    {
                        totalItem++;
                        totalPrice += product.Price;

                        cartSummary.CartItems.Add(new CartViewModel
                        {
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            AddedDate= item.AddedDate,
                            Discount = product.Discount,
                            Description = product.Description,
                            Price = product.Price,
                            ProductId = product.ProductId,
                            Message = $"Product '{product.ProductName}' is in your cart."
                        });
                    }
                }
                cartSummary.TotalItems = totalItem;
                cartSummary.TotalPrice = totalPrice;

            }
            return cartSummary;
        }

        public void RemoveAllCart(string userName)
        {
            if (Carts.ContainsKey(userName))
            {
                Carts[userName].Clear();
            }
        }

        public void RemoveItemsInCart(string userName, int productId)
        {
            if (Carts.ContainsKey(userName))
            {
                var cart = Carts[userName];
                var existingItem = cart.Find(x => x.ProductId == productId);
                if (existingItem != null) 
                {
                    throw new InvalidOperationException($"Product with ID {productId} not exist in cart!");
                }
                cart.RemoveAll(x => x.ProductId == productId);
            }
            else
            {
                throw new InvalidOperationException("User's cart does not exist.");
            }
        }
    }
}
