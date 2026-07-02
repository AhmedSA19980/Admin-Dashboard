using Microsoft.AspNetCore.Authorization;

namespace Admin.API.Authorization
{
    public class UserOwnerOrManagerOrAdminRequirment :IAuthorizationRequirement
    {
    }
}
