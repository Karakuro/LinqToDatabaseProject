using Microsoft.AspNetCore.Identity;

namespace LinqToDatabaseProject.Data
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? Birthday { get; set; }
    }
}
