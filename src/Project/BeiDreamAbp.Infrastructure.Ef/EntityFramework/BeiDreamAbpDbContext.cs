using System.Data.Common;
using System.Data.Entity;
using Abp.EntityFramework;
using BeiDreamAbp.Domain;
using BeiDreamAbp.Domain.User;

namespace BeiDreamAbp.Infrastructure.Ef.EntityFramework
{
    /// <summary>
    /// 数据库访问上下文,继承AbpDbContext
    /// </summary>
    public class BeiDreamAbpDbContext : AbpDbContext
    {
        public virtual IDbSet<User> Users { get; set; }

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