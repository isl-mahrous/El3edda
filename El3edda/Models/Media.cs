using Microsoft.EntityFrameworkCore;
using El3edda.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace El3edda.Models
{  
    public class Media
    {
        [Required]
        [EnumDataType(typeof(MediaType))]
        public MediaType Type { get; set; }
        [Required]
        [DataType(DataType.Url)]
        public string URL { get; set; }
    }
}