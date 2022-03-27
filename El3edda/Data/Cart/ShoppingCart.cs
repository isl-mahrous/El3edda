using El3edda.Models;
using Microsoft.EntityFrameworkCore;

namespace El3edda.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services
                .GetRequiredService<IHttpContextAccessor>()
                ?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string CartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", CartId);
            return new ShoppingCart(context) { ShoppingCartId = CartId };
        }

        public void AddItemToCart(Mobile mobile)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(
                n => n.Mobile.Id == mobile.Id && n.ShoppingCartId == ShoppingCartId
            );

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Mobile = mobile,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Mobile mobile)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(
                n => n.Mobile.Id == mobile.Id && n.ShoppingCartId == ShoppingCartId
            );

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();
        }

        public void RemoveAllItemFromCart(Mobile mobile)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(
                n => n.Mobile.Id == mobile.Id && n.ShoppingCartId == ShoppingCartId
            );

            if (shoppingCartItem != null)
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }

            _context.SaveChanges();
        }

        public void ClearCart()
        {
            _context.ShoppingCartItems.RemoveRange(_context.ShoppingCartItems);
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems
                ?? (
                    ShoppingCartItems = _context.ShoppingCartItems
                        .Where(n => n.ShoppingCartId == ShoppingCartId)
                        .Include(n => n.Mobile)
                        .ToList()
                );
        }

        public double GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Mobile.Price * n.Amount)
                .Sum();
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
