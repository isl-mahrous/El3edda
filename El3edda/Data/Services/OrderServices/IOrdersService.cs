using El3edda.Models;

namespace El3edda.Data.Services.OrderServices
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(
            List<ShoppingCartItem> items,
            string userId,
            string email,
            Address ShippingAddress
        );

        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
