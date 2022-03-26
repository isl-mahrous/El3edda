using Microsoft.EntityFrameworkCore;

namespace El3edda.Models
{
    [Owned]
    public class Address
    {
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
    }
}
