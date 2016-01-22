using Abp.MultiTenancy;
using BeiDreamAbp.Domain.Users;

namespace BeiDreamAbp.Domain.MultiTenancy
{
    public class Tenant : AbpTenant<Tenant, User>
    {
        public Tenant()
        {

        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}