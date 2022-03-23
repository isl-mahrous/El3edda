using El3edda.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El3edda.Models
{
    [Index(nameof(Serial), IsUnique = true)]
    public class Mobile : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        
        
        [Required]
        public int Serial { get; set; }
        [Required]
        [StringLength(maximumLength:50, ErrorMessage = "Phone name cannot be more than 50 characters.")]
        public string Name { get; set; }
        
        
        [Display(Name ="Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        
        
        [Required]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        
        
        [Required]
        public double Price { get; set; }
        
        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }

        
        [DataType(DataType.Duration)]
        public TimeSpan WarrantyPeriod { get; set; }

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
