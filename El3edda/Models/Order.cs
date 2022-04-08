using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using El3edda.Data.Enums;

namespace El3edda.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Address ShippingAddress { get; set; }
        [EnumDataType(typeof(OrderState))]
        public OrderState orderState { get; set; } = OrderState.confirmed;

        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }
}
