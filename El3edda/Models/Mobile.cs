using El3edda.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El3edda.Models
{

    public class Mobile : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        
        
        [Required]
        [StringLength(maximumLength:50, ErrorMessage = "Phone name cannot be more than 50 characters.")]
        public string Name { get; set; }
        
        
        [Display(Name ="Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        
        
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        
        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }


        [Display(Name= "Warranty Period (Months)")]
        public int WarrantyPeriod { get; set; }

        [Required]
        [Display(Name="Units In Stock")] 
        public int UnitsInStock { get; set; }
        
        [Required]
        [Display(Name = "Main Photo")]
        [DataType(DataType.ImageUrl)]
        public string MainPhotoURL { get; set; }

        [Required]
        [Display(Name = "Units Sold")]
        public int UnitsSold { get; set; }

        [Required]
        [Display(Name = "Specifications")]
        public Specs Specs { get; set; }


        //Relatonships
        [Required]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Media> Media { get; set; }


    }
}
