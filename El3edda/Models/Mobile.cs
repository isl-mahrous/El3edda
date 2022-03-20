using El3edda.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace El3edda.Models
{
    public class Mobile : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50, ErrorMessage = "Phone name cannot be more than 50 characters.")]
        public string Name { get; set; }
        [Display(Name ="Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public Manufacturer Manufacturer { get; set; }
        [Required]
        public double Price { get; set; }
        [StringLength(maximumLength: 200, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }

        [Required]
        public int WarrantyPeriod { get; set; }

        [Required]
        public int UnitsInStock { get; set; }
        [Required]
        public int UnitsSold { get; set; }

        //Relatonships
        [Required]
        public Specs Specs { get; set; }
        [Required]
        public ICollection<Media> Media { get; set; }


    }
}
