using KwiqBlog.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KwiqBlog.Models.BlogViewModels
{
    public class EditViewModel
    {
        [Display(Name = "Header Image")]
        public IFormFile BlogHeaderImg { get; set; }
        public Blog Blog { get; set; }
    }
}
