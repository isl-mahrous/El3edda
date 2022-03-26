using Microsoft.EntityFrameworkCore;
using El3edda.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using El3edda.Data.Base;

namespace El3edda.Models
{  

    public class Media : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [EnumDataType(typeof(MediaType))]
        public MediaType Type { get; set; }
        
        [Required]
        public string URL { get; set; }

        [Required]
        [ForeignKey("Mobile")]
        public int MobileId { get; set; }

        public virtual Mobile Mobile { get; set; }
    }
}