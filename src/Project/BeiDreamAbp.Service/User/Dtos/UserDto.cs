using Abp.Application.Services.Dto;

namespace BeiDreamAbp.Service.User.Dtos  
{
    /// <summary>
    /// 用户数据传输对象
    /// </summary>
    public class UserDto : EntityDto<long>
    {
        public string Name { get; set; }    
    }
}