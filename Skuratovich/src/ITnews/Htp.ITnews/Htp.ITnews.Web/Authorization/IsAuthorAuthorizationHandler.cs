using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Web.Authorization.Requirements;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Htp.ITnews.Web.Authorization
{
    public class IsAuthorAuthorizationHandler : AuthorizationHandler<SameAuthorRequirement, IResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                            SameAuthorRequirement requirement,
                                            IResource resource)
        {

            if (context.User.GetUserId() == resource.AuthorId.ToString())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
