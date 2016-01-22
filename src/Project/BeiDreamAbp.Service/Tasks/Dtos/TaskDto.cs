using Abp.Application.Services.Dto;

namespace BeiDreamAbp.Service.Tasks.Dtos  
{
    /// <summary>
    /// 用户数据传输对象
    /// </summary>
    public class TaskDto : EntityDto<long>
    {
        public string Name { get; set; }    
    }
}