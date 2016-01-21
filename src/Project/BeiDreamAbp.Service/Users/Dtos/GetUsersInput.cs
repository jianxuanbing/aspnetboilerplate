using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace BeiDreamAbp.Service.Users.Dtos
{
    public class GetUsersInput : IInputDto
    {
        [Required]
        public string Name { get; set; }
    }
}