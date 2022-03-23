using El3edda.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace El3edda.Models
{
    public class Manufacturer  : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50,
            ErrorMessage = "Manufacturer name cannot be more than 50 characters.")]
        public string Name { get; set; }


        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Orgin cannot be more than 50 characters.")]
        public string Origin { get; set; }

        public virtual ICollection<Mobile> Mobiles { get; set; } = new HashSet<Mobile>();
    }
}