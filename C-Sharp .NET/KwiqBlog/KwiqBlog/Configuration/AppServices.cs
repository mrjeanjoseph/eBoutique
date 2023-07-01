using KwiqBlog.BusinessManagers;
using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Data;
using KwiqBlog.Data.Models;
using KwiqBlog.Services;
using KwiqBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace KwiqBlog.Configuration
{
    public static class AppServices
    {
        public static void AddDefaultServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConn")));

            serviceCollection.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            serviceCollection.AddControllersWithViews().AddRazorRuntimeCompilation();

            serviceCollection.AddRazorPages();

            serviceCollection.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }

        public static void AddCustomServices(this IServiceCollection serviceCollection)
        {
            //These two are running errors like crazy - This cause migration to stop running
            serviceCollection.AddScoped<IBlogBusinessManager, BlogBusinessManager>();

            serviceCollection.AddScoped<IBlogService, BlogService>();
        }
    }
}
