using Microsoft.AspNetCore.Identity;

namespace KwiqBlog.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData] public string firstName {  get; set; }
        [PersonalData] public string lastName {  get; set; }
    }
}
