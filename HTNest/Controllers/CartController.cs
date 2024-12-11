using HTNest.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HTNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]

    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        // GET: api/<CartController>
        [HttpGet("Get-Cart")]
        public IActionResult GetCart()
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                userName = HttpContext.Session.GetString("GuestId");
                if (string.IsNullOrEmpty(userName))
                {
                    return BadRequest("User Id or guest id is required");
                }
            }
            var cartSummary = _cartService.GetCart(userName);
            return Ok(cartSummary);
        }

       

        // POST api/<CartController>
        [HttpPost("Add-cart")]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                if (HttpContext.Session.GetString("GuestId") == null)
                {
                    var guestId = "Gues_" + Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("GuestId", guestId);
                    userName = guestId;
                }
                else
                {
                    userName = HttpContext.Session.GetString("GuestId");
                }
            }
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "Manager" || userRole == "Admin")
            {
                return StatusCode(403, "You do not have permission to add items to the cart.");
            }
            try
            {
                var result = await _cartService.AddToCart(userName, productId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartController>/5
        [HttpDelete("clear")]
        public IActionResult clearAllCart()
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                userName = HttpContext.Session.GetString("GuestId");
                if (string.IsNullOrEmpty(userName))
                {
                    return BadRequest("User ID or Guest ID is required");
                }
            }
            _cartService.RemoveAllCart(userName);
            return Ok("Cart cleared successfully");
        }

        [HttpDelete("remove/{prodcutId}")]
        public IActionResult RemoveItemInCart(int productId)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userName))
            {
                userName = HttpContext.Session.GetString("GuestId");
                if (string.IsNullOrEmpty(userName))
                {
                    return BadRequest("User Name or Guest Id is required.");
                }
            }
            try
            {
                _cartService.RemoveItemsInCart(userName, productId);
                return Ok("Item removed from cart successfully");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
