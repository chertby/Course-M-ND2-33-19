using System.Threading.Tasks;
using Htp.ITnews.Web.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace Htp.ITnews.Web.Authorization.Handlers
{
    public class AdministratorHandler : AuthorizationHandler<EditRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                            EditRequirement requirement)
        {

            if (context.User.IsInRole("Administrator"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
