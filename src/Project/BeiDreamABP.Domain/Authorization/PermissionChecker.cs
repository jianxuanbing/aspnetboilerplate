using Abp.Authorization;
using BeiDreamAbp.Domain.Authorization.Roles;
using BeiDreamAbp.Domain.Authorization.Users;
using BeiDreamAbp.Domain.MultiTenancy;

namespace BeiDreamAbp.Domain.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}