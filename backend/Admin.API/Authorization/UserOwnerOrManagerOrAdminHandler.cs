using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Admin.API.Authorization
{
    public class UserOwnerOrManagerOrAdminHandler:AuthorizationHandler<UserOwnerOrManagerOrAdminRequirment, int>
    {
        protected override Task HandleRequirementAsync(
       AuthorizationHandlerContext context,
       UserOwnerOrManagerOrAdminRequirment requirement,
       int studentId)
        {
            // Admin override
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            // Ownership check
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userId, out int authenticatedStudentId) &&
                authenticatedStudentId == studentId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
