using Microsoft.EntityFrameworkCore;
using System.Drawing;
using El3edda.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace El3edda.Models
{
    [Owned]
    public class Specs
    {
        
        public string CPU { get; set; }
        public ScreenEnum Screen { get; set; }
        public Dimension Dimensions { get; set; }
        public double CameraMegaPixels { get; set; }
        public MobileColor Color { get; set; }
        public double Weight { get; set; }
        public OSEnum OS { get; set; }
        public int BatteryCapacity { get; set; }


    }
}