using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;
using BeiDreamAbp.Domain.User;

namespace BeiDreamAbp.Infrastructure.Ef.EntityFramework.Repositories
{
    public class UserRepository : BeiDreamAbpRepositoryBase<User, long>, IUserRepository
    {
        public UserRepository(IDbContextProvider<BeiDreamAbpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<User> GetUsers(string name)
        {
            var query = GetAll();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }
            return query.OrderByDescending(p=>p.Name).ToList();
        }
    }
}