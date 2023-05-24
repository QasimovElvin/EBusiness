
using Microsoft.AspNetCore.Identity;

namespace EBusiness.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; }=null!;
    }
}
