using KwiqBlog.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace KwiqBlog.Authorization
{
    public class PostAuthHandler : AuthorizationHandler<OperationAuthorizationRequirement, Post>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PostAuthHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Post resource)
        {
            var applicationUser = await _userManager.GetUserAsync(context.User);
            if((requirement.Name == PostOperations.Update.Name || requirement.Name == PostOperations.Delete.Name) && applicationUser == resource.PostCreator)
            {
                context.Succeed(requirement);
            }
        }
    }
}
