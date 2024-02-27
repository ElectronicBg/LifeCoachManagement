using Microsoft.AspNetCore.Identity;

namespace LifeCoachManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
