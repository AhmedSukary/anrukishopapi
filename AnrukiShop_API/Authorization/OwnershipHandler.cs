using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public class OwnershipHandler : AuthorizationHandler<OwnershipRequirement, int>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnershipRequirement requirement, int targetId)
    {
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (int.TryParse(userId, out int authenticatedUserId) && authenticatedUserId == targetId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}