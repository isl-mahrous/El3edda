using Microsoft.EntityFrameworkCore;
using El3edda.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace El3edda.Models
{  
    public class Media
    {
        [Required]
        public MediaType Type { get; set; }
        
        [Required]
        public string URL { get; set; }
    }
}