using System.Data.Entity.Migrations;
using BeiDreamAbp.Domain;
using BeiDreamAbp.Domain.Users;
using BeiDreamAbp.Infrastructure.Ef.EntityFramework;

namespace BeiDreamAbp.Infrastructure.Ef.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BeiDreamAbpDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BeiDreamAbpDbContext context)
        {
            context.Users.AddOrUpdate(p => p.Name, new User() {Name = "AAAA"}, new User { Name = "Douglas Adams" });
        }
    }
}