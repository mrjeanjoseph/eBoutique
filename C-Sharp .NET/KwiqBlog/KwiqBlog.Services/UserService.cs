using KwiqBlog.Data;
using KwiqBlog.Data.Models;
using KwiqBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KwiqBlog.Services {
    public class UserService: IUserService {
        private readonly ApplicationDbContext _appDbContext;

        public UserService(ApplicationDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        public async Task<ApplicationUser> Update(ApplicationUser appUser) {
            _appDbContext.Update(appUser);
            await _appDbContext.SaveChangesAsync();
            return appUser;
        }
    }
}
