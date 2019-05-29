using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Web.Authorization.Requirements;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Htp.ITnews.Web.Authorization.Handlers
{
    public class SameAuthorHandler : AuthorizationHandler<EditRequirement, IResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                            EditRequirement requirement,
                                            IResource resource)
        {

            if (context.User.GetUserId() == resource.AuthorId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
