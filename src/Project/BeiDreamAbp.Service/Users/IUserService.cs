using Abp.Application.Services;
using BeiDreamAbp.Service.Users.Dtos;

namespace BeiDreamAbp.Service.Users
{
    public interface IUserService : IApplicationService
    {
        GetUsersOutput GetUsers(GetUsersInput input);
    }
}