using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace BeiDreamAbp.Service.User.Dtos
{
    public class GetUsersOutput : IOutputDto
    {
        public List<UserDto> Users { get; set; }    
    }
}