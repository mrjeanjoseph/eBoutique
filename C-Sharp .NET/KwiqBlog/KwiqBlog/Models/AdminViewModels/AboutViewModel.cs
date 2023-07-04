using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KwiqBlog.Models.AdminViewModels {
    public class AboutViewModel {
        [Display(Name = "Header Image")] public IFormFile HeaderImg { get; set; }
        [Display(Name = "Sub-header")] public string SubHeader { get; set; }
        public string Content { get; set; }
    }
}
