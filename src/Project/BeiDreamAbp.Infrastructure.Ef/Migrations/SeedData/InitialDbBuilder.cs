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

            new DefaultEditionCreator(_context).Create();
            //new DefaultLanguagesCreator(_context).Create();
            new DefaultTenantRoleAndUserCreator(_context).Create();
            //new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}