using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Authorization.Requirements;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Htp.ITnews.Web.Authorization.Handlers
{
    public class SameUserHandler : AuthorizationHandler<EditRequirement, UserViewModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                            EditRequirement requirement,
                                            UserViewModel resource)
        {

            if (context.User.GetUserId() == resource.Id)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
