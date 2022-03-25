using El3edda.Data.Static;
using El3edda.Models;
using Microsoft.EntityFrameworkCore;

namespace El3edda.Data.Services.OrderServices
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Mobile).Include(o => o.User).ToListAsync();
            if (userRole != UserRoles.Admin)
            {
                orders = orders.Where(o => o.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string email)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = email,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MobileId = item.Mobile.Id,
                    OrderId = order.Id,
                    Price = item.Mobile.Price
                };

                //Decrement Units in stock 
                item.Mobile.UnitsInStock -= item.Amount;

                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
