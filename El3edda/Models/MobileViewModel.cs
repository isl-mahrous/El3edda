using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El3edda.Models
{
    public class MobileViewModel
    {
        //[Required]
        //[Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Phone name cannot be more than 50 characters.")]
        [Display(Name ="Mobile's Name")]
        public string Name { get; set; }


        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        [StringLength(maximumLength: 500, ErrorMessage = "Description is too long."), MinLength(20, ErrorMessage ="Minimum Length is 20 characters")]
        public string Description { get; set; }


        [Display(Name = "Warranty Period (month)")]
        public int WarrantyPeriod { get; set; }

        [Required]
        [Display(Name = "Units In Stock")]
        public int UnitsInStock { get; set; }

        [Display(Name = "Main Photo")]
        public IFormFile MainPhotoURL { get; set; }

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

        public Manufacturer Manufacturer { get; set; }
        [Display(Name ="Other Photos")]
        public List<IFormFile> Media { get; set; }

    }
}
