using El3edda.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace El3edda.Models
{
    public class Manufacturer // : IEntityBase or owned entity?
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50, 
            ErrorMessage = "Manufacturer name cannot be more than 50 characters.")]
        public string Name { get; set; }
        [StringLength(maximumLength: 50,
            ErrorMessage = "Orgin cannot be more than 50 characters.")]
        public string? Origin { get; set; }
    }
}