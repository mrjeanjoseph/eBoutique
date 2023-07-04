using KwiqBlog.Data;
using KwiqBlog.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KwiqBlog.Services.Interfaces {
    public interface IUserService {
        Task<ApplicationUser> Update(ApplicationUser appUser);

    }
}
