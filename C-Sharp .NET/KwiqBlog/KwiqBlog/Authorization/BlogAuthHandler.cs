using KwiqBlog.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace KwiqBlog.Authorization
{
    public class BlogAuthHandler : AuthorizationHandler<OperationAuthorizationRequirement, Post>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogAuthHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Post resource)
        {
            var applicationUser = await _userManager.GetUserAsync(context.User);
            if((requirement.Name == BlogOperations.Update.Name || requirement.Name == BlogOperations.Delete.Name) && applicationUser == resource.BlogCreator)
            {
                context.Succeed(requirement);
            }
        }
    }
}
