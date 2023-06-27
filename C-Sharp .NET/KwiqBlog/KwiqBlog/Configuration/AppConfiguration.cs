using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace KwiqBlog.Configuration
{
    public static class AppConfiguration
    {
        public static void AddDefaultConfiguration(this IApplicationBuilder appBuilder, IWebHostEnvironment webHostEnv)
        {
            if (webHostEnv.IsDevelopment())
            {
                appBuilder.UseDeveloperExceptionPage();
                appBuilder.UseDatabaseErrorPage();
            } else
            {
                appBuilder.UseExceptionHandler("/Home/Error");
                appBuilder.UseHsts();
            }
            appBuilder.UseHttpsRedirection();
            appBuilder.UseStaticFiles();

            appBuilder.UseRouting();

            appBuilder.UseAuthentication();
            appBuilder.UseAuthorization();

            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
