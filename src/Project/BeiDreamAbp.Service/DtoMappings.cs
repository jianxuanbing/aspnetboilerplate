using AutoMapper;
using BeiDreamAbp.Domain.User;
using BeiDreamAbp.Service.User;
using BeiDreamAbp.Service.User.Dtos;

namespace BeiDreamAbp.Service
{
    internal static class DtoMappings
    {
        public static void Map()
        {
            Mapper.CreateMap<User, UserDto>();
        }
    }
}