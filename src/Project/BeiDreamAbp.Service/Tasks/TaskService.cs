using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Authorization;
using AutoMapper;
using BeiDreamAbp.Domain.Tasks;
using BeiDreamAbp.Service.Tasks.Dtos;

namespace BeiDreamAbp.Service.Tasks
{
    [AbpAuthorize]
    public class TaskService : BeiDreamAbpAppServiceBase, ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public GetTasksOutput GetTasks(GetTasksInput input)
        {
            var users = _taskRepository.GetTasks(input.Name);
            return new GetTasksOutput()
            {
                Tasks = Mapper.Map<List<TaskDto>>(users)
            };
        }
    }
}