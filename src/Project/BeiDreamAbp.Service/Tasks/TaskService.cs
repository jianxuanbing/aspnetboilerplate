using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Events.Bus;
using AutoMapper;
using BeiDreamAbp.Domain.Tasks;
using BeiDreamAbp.Service.Tasks.Dtos;
using Task = System.Threading.Tasks.Task;

namespace BeiDreamAbp.Service.Tasks
{
    [AbpAuthorize]
    public class TaskService : BeiDreamAbpAppServiceBase, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public IEventBus EventBus { get; set; }

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
            EventBus = NullEventBus.Instance;
        }

        public GetTasksOutput GetTasks(GetTasksInput input)
        {
            var users = _taskRepository.GetTasks(input.Name);
            return new GetTasksOutput()
            {
                Tasks = Mapper.Map<List<TaskDto>>(users)
            };
        }

        public async Task CreateOrUpdateTasks(GetTasksInput input)
        {
            //await _taskRepository.InsertAsync(new Domain.Tasks.Task { Name = input.Name });
            EventBus.Trigger(new TaskCompletedEventData { TaskId = 1 });
        }
    }
    public class TaskCompletedEventData : EventData
    {
        public int TaskId { get; set; }
    }
}