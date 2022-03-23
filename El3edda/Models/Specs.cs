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
        public ScreenEnum Screen { get; set; }
        
        
        public double Height { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        
        
        public double CameraMegaPixels { get; set; }

        [Required]
        [EnumDataType(typeof(Colors))]
        public Colors Color { get; set; }
        public double Weight { get; set; }


        [EnumDataType(typeof(OSEnum))]

        public OSEnum OS { get; set; }
        public int BatteryCapacity { get; set; }

    }
}