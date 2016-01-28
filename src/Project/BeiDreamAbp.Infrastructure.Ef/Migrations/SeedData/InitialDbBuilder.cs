using BeiDreamAbp.Infrastructure.Ef.EntityFramework;
using EntityFramework.DynamicFilters;

namespace BeiDreamAbp.Infrastructure.Ef.Migrations.SeedData
{
    public class InitialDbBuilder
    {
        private readonly BeiDreamAbpDbContext _context;

        public InitialDbBuilder(BeiDreamAbpDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionBuilder(_context).Build();
            new DefaultLanguagesBuilder(_context).Build();
            new DefaultTenantRoleAndUserBuilder(_context).Build();
            new DefaultSettingsBuilder(_context).Build();

            _context.SaveChanges();
        }
    }
}