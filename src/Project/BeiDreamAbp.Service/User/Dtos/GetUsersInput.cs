using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace BeiDreamAbp.Service.User.Dtos
{
    public class GetUsersInput : IInputDto
    {
        [Required]
        public string Name { get; set; }
    }
}