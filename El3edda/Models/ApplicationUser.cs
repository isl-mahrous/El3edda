using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace El3edda.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        public Address ShippingAddress { get; set; }
    }
}
