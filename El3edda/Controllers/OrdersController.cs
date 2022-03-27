using El3edda.Data.Cart;
using El3edda.Data.Services.MobileService;
using El3edda.Data.Services.OrderServices;
using El3edda.Data.ViewModels;
using El3edda.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(
            IMobileService service,
            ShoppingCart shoppingCart,
            IOrdersService ordersService,
            UserManager<ApplicationUser> userManager
        )
        {
            _mobileService = service;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
            _userManager = userManager;
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

        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);

            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new CheckoutVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
                ShippingAddress = user.ShippingAddress ?? new Address()
            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutVM checkoutVM)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            var userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _ordersService.StoreOrderAsync(
                items,
                userId,
                userEmailAddress,
                checkoutVM.ShippingAddress
            );

            await _shoppingCart.ClearShoppingCartAsync();
            return View("CompleteOrder");
        }

        public IActionResult Charge(string stripeEmail, string stripeToken, int total)
        {
            var customers = new Stripe.CustomerService();
            var charges = new Stripe.ChargeService();

            var customer = customers.Create(
                new Stripe.CustomerCreateOptions { Email = stripeEmail, Source = stripeToken }
            );

            var charge = charges.Create(
                new Stripe.ChargeCreateOptions
                {
                    Amount = total,
                    Description = "Order Payment",
                    Currency = "USD",
                    Customer = customer.Id
                }
            );

            if (charge.Status == "succeeded")
            {
                return RedirectToAction("CompleteOrder");
            }
            else
            {
                return RedirectToAction("ShoppingCart");
            }
        }
    }
}
