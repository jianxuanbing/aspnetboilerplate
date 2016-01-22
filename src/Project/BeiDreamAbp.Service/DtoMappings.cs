using AutoMapper;
using BeiDreamAbp.Domain.Tasks;
using BeiDreamAbp.Service.Tasks.Dtos;

namespace BeiDreamAbp.Service
{
    internal static class DtoMappings
    {
        public static void Map()
        {
            Mapper.CreateMap<Task, TaskDto>();
        }
    }
}