﻿using System.Threading.Tasks;
using Abp.Application.Services;
using BeiDreamAbp.Service.Tasks.Dtos;

namespace BeiDreamAbp.Service.Tasks
{
    public interface ITaskService : IApplicationService
    {
        GetTasksOutput GetTasks(GetTasksInput input);
        Task CreateOrUpdateTasks(GetTasksInput input);
    }
}