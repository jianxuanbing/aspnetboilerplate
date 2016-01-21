using System.Collections.Generic;
using Abp.Application.Services;
using AutoMapper;
using BeiDreamAbp.Domain.Users;
using BeiDreamAbp.Service.Users.Dtos;

namespace BeiDreamAbp.Service.Users
{
    public class UserService : ApplicationService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public GetUsersOutput GetUsers(GetUsersInput input)
        {
            var users = _userRepository.GetUsers(input.Name);
            return new GetUsersOutput()
            {
                Users = Mapper.Map<List<UserDto>>(users)
            };
        }
    }
}