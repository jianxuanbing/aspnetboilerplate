using AutoMapper;
using BeiDreamAbp.Domain.Users;
using BeiDreamAbp.Service.Users.Dtos;

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