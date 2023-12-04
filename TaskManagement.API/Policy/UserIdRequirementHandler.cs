using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagement.API.Policy
{
    public class UserIdRequirementHandler : AuthorizationHandler<UserIdRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            UserIdRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "UserId"))
            {
                var userId = int.Parse(context.User.FindFirst(c => c.Type == "UserId").Value);

                if (userId == requirement.RequiredUserId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}