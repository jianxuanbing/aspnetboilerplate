using System.Collections.Generic;
using Abp.Domain.Repositories;

namespace BeiDreamAbp.Domain.Tasks
{
    public interface ITaskRepository: IRepository<Task, long>
    {
        List<Task> GetTasks(string name);   
    }
}