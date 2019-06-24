using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Htp.ITnews.Web.Authorization.Requirements;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Htp.ITnews.Web.Authorization
{
    public class PermissionHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();

            foreach (var requirement in pendingRequirements)
            {
                if (requirement is EditPermission)
                {
                    if (IsAuthor(context.User, context.Resource))
                    {
                        context.Succeed(requirement);
                    }
                }
                //else if (requirement is EditPermission ||
                //         requirement is DeletePermission)
                //{
                //    if (IsOwner(context.User, context.Resource))
                //    {
                //        context.Succeed(requirement);
                //    }
                //}
            }

            return Task.CompletedTask;
        }

        private bool IsAuthor(ClaimsPrincipal user, object resource)
        {
            //return user.GetUserId() == resource.

            return true;
        }
    }
}
