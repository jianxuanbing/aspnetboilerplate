using Abp.Application.Features;
using BeiDreamAbp.Domain.Authorization.Roles;
using BeiDreamAbp.Domain.Authorization.Users;
using BeiDreamAbp.Domain.MultiTenancy;

namespace BeiDreamAbp.Domain.Features
{
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, Role, User>
    {
        public FeatureValueStore(TenantManager tenantManager)
            : base(tenantManager)
        {
        }
    }
}