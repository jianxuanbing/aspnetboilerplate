using System.Threading.Tasks;
using Abp.Application.Services;
using BeiDreamAbp.Service.Authorization.Users.Profile.Dto;

namespace BeiDreamAbp.Service.Authorization.Users.Profile
{
    public interface IProfileAppService : IApplicationService
    {       
        Task ChangePassword(ChangePasswordInput input);
    }
}
