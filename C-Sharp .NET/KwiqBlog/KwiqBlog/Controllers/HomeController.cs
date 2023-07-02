using KwiqBlog.BusinessManagers;
using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KwiqBlog.Controllers {
    public class HomeController : Controller {
        private readonly IBlogBusinessManager _blogBusinessManager;

        public HomeController(IBlogBusinessManager blogBusinessManager) {
            _blogBusinessManager = blogBusinessManager;
        }

        public IActionResult Index(string searchStr, int? page) {
            return View(_blogBusinessManager.GetIndexViewModel(searchStr, page));
        }
    }
}
