using Microsoft.EntityFrameworkCore;

namespace El3edda.Models
{

    [Owned]
    public class Dimension
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
    }
}
