using Microsoft.EntityFrameworkCore;
using System.Drawing;
using El3edda.Data.Enums;
namespace El3edda.Models
{
    [Owned]
    public class Specs
    {
        public string CPU { get; set; }
        public ScreenEnum Screen { get; set; }
        public Dimension Dimensions { get; set; }
        public double CameraMegaPixels { get; set; }
        public Color Color { get; set; }
        public double Weight { get; set; }
        public OSEnum OS { get; set; }
        public int BatteryCapacity { get; set; }


    }
}