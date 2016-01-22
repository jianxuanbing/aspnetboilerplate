using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace BeiDreamAbp.Service.Tasks.Dtos
{
    public class GetTasksInput : IInputDto
    {
        [Required]
        public string Name { get; set; }
    }
}