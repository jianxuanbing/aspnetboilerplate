using Abp.Authorization.Roles;
using BeiDreamAbp.Domain.MultiTenancy;
using BeiDreamAbp.Domain.Users;

namespace BeiDreamAbp.Domain.Authorization.Roles
{
    public class Role : AbpRole<Tenant, User>
    {

    }
}