using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using BeiDreamAbp.Domain.Authorization.Roles;
using BeiDreamAbp.Domain.Authorization.Users;

namespace BeiDreamAbp.Domain.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            EditionManager editionManager)
            : base(
                tenantRepository,
                tenantFeatureRepository,
                editionManager
            )
        {
        }
    }
}