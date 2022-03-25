using El3edda.Data.Cart;
using El3edda.Data.Services.MobileService;
using El3edda.Data.Services.OrderServices;
using El3edda.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace El3edda.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMobileService _mobileService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IMobileService service, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _mobileService = service;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            var result = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(result);
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var mobile = await _mobileService.GetByIdAsync(id);
            if (mobile != null)
            {
                _shoppingCart.AddItemToCart(mobile);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var mobile = await _mobileService.GetByIdAsync(id);
            if (mobile != null)
            {
                _shoppingCart.RemoveItemFromCart(mobile);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            var userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("CompleteOrder");
        }
    }
}
