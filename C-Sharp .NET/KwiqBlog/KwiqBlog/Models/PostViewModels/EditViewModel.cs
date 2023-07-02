using KwiqBlog.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KwiqBlog.Models.PostViewModels {
    public class EditViewModel
    {
        [Display(Name = "Header Image")]
        public IFormFile HeaderImg { get; set; }
        public Post Post { get; set; }
    }
}
