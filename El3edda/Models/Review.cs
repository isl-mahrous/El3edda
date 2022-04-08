using El3edda.Data.Base;
using El3edda.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El3edda.Models
{
    public class Review : IEntityBase
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage ="Please enter your name")]
        [Display(Name ="Your Name")]
        public string CustomerName { get; set; }

        [Required]
        [EnumDataType(typeof(MobileRate))]
        [Display(Name = "Mobile To Review")]
        public MobileRate Rate { get; set; }


        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage ="Your Review must be of length between 3 and 150 characters")]
        [Display(Name = "Your Review")]
        public string Feedback { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("Mobile")]
        public int MobileId { get; set; }

        public virtual Mobile Mobile { get; set; }
    }
}
