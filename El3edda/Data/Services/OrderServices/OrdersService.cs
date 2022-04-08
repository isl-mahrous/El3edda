using El3edda.Data.Enums;
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
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Mobile)
                .Include(o => o.User)
                .ToListAsync();
            if (userRole != UserRoles.Admin)
            {
                orders = orders.Where(o => o.UserId == userId).ToList();
            }
            return orders;
        }
        /// <summary>
        /// cancel an order and update the mobile stock
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task cancelOrderAsync(int orderId)
        {

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i=>i.Mobile)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            order.orderState = OrderState.cancelled;
            foreach (var item in order.OrderItems)
            {
                //Decrement Units Sold
                item.Mobile.UnitsSold -= item.Amount;

                //Increment Units in stock
                item.Mobile.UnitsInStock += item.Amount;
            }

            await _context.SaveChangesAsync();

        }
        public async Task StoreOrderAsync(
            List<ShoppingCartItem> items,
            string userId,
            string email,
            Address shippingAddress
        )
        {
            var order = new Order()
            {
                UserId = userId,
                Email = email,
                ShippingAddress = shippingAddress
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

                //Increment Units Sold
                item.Mobile.UnitsSold += item.Amount;

                //TODO handel if units in stock not enough
                //Decrement Units in stock
                item.Mobile.UnitsInStock -= item.Amount;

                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
