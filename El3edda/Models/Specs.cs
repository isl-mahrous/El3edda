using Microsoft.EntityFrameworkCore;
using System.Drawing;
using El3edda.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace El3edda.Models
{
    [Owned]
    public class Specs
    {
        [Required]
        public string CPU { get; set; }
        
        [Required]
        [EnumDataType(typeof(ScreenEnum))]
        [Display(Name ="Screen Type")]
        public ScreenEnum Screen { get; set; }
        
        
        public double Height { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        
        [Display(Name ="Camera MegaPixels")]
        public double CameraMegaPixels { get; set; }

        [Required]
        [EnumDataType(typeof(Colors))]
        public Colors Color { get; set; }
        public double Weight { get; set; }


        [EnumDataType(typeof(OSEnum))]
        [Display(Name ="Operating System")]
        public OSEnum OS { get; set; }
        [Display(Name = "Battery Capacity")]
        public int BatteryCapacity { get; set; }
        
        
        [Display(Name = "RAM"), Required]
        public int RAM { get; set; }
        [Display(Name = "Storage"), Required]
        public int Storage { get; set; }

        //TODO
        //Mobile RAM and ROM views and Model

    }
}