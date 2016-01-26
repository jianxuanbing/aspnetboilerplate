using System.Data.Entity.Migrations;
using BeiDreamAbp.Domain;
using BeiDreamAbp.Domain.Tasks;
using BeiDreamAbp.Infrastructure.Ef.EntityFramework;
using BeiDreamAbp.Infrastructure.Ef.Migrations.SeedData;

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
            new InitialDbBuilder(context).Create();
            //context.Tasks.AddOrUpdate(p => p.Name, new Task() {Name = "AAAA"}, new Task { Name = "Douglas Adams" });
        }
    }
}