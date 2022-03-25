using System.ComponentModel.DataAnnotations;

namespace El3edda.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
        public virtual Mobile Mobile { get; set; }
    }
}
