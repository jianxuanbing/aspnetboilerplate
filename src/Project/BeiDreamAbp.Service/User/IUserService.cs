using Abp.Application.Services;
using BeiDreamAbp.Service.User.Dtos;

namespace BeiDreamAbp.Service.User
{
    public interface IUserService : IApplicationService
    {
        GetUsersOutput GetUsers(GetUsersInput input);
    }
}