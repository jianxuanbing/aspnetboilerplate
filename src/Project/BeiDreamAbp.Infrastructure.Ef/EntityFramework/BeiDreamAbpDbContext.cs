using System.Data.Common;
using System.Data.Entity;
using Abp.EntityFramework;
using Abp.Zero.EntityFramework;
using BeiDreamAbp.Domain;
using BeiDreamAbp.Domain.Authorization.Roles;
using BeiDreamAbp.Domain.Authorization.Users;
using BeiDreamAbp.Domain.MultiTenancy;
using BeiDreamAbp.Domain.Tasks;

namespace BeiDreamAbp.Infrastructure.Ef.EntityFramework
{
    /// <summary>
    /// 数据库访问上下文,继承AbpDbContext
    /// </summary>
    public class BeiDreamAbpDbContext:AbpZeroDbContext<Tenant, Role, User>
    {
        public virtual IDbSet<Task> Tasks { get; set; }

        public BeiDreamAbpDbContext()
            : base("Default")
        {

        }

        public BeiDreamAbpDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public BeiDreamAbpDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}