using Microsoft.AspNetCore.Identity;

namespace KwiqBlog.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string firstName {  get; set; }
        public string lastName {  get; set; }
    }
}
