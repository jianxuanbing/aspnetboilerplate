using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace BeiDreamAbp.Service.Users.Dtos
{
    public class GetUsersOutput : IOutputDto
    {
        public List<UserDto> Users { get; set; }    
    }
}