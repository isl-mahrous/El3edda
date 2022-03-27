using El3edda.Data.Cart;
using El3edda.Models;

namespace El3edda.Data.ViewModels
{
    public class CheckoutVM
    {
        public ShoppingCart ShoppingCart { get; set; }
        public double ShoppingCartTotal { get; set; }
        public Address ShippingAddress { get; set; }
    }
}
