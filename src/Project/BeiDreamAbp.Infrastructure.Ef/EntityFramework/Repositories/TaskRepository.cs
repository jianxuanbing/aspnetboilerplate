using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;
using BeiDreamAbp.Domain.Tasks;

namespace BeiDreamAbp.Infrastructure.Ef.EntityFramework.Repositories
{
    public class TaskRepository : BeiDreamAbpRepositoryBase<Task, long>, ITaskRepository
    {
        public TaskRepository(IDbContextProvider<BeiDreamAbpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<Task> GetTasks(string name)
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