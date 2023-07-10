using KwiqBlog.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KwiqBlog.Models.AdminViewModels {
    public class AboutViewModel {
        public ApplicationUser AppUser { get; set; }
        [Display(Name = "Header Image")] public IFormFile HeaderImg { get; set; }
        [Display(Name = "Sub-header")] public string SubHeader { get; set; }
        public string Content { get; set; }
    }
}
