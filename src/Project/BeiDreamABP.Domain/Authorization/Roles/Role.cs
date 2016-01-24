using Abp.Authorization.Roles;
using BeiDreamAbp.Domain.Authorization.Users;
using BeiDreamAbp.Domain.MultiTenancy;

namespace BeiDreamAbp.Domain.Authorization.Roles
{
    public class Role : AbpRole<Tenant, User>
    {

    }
}