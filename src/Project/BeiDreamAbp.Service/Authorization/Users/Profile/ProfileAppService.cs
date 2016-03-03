using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using BeiDreamAbp.Service.Authorization.Users.Profile.Dto;

namespace BeiDreamAbp.Service.Authorization.Users.Profile
{
    [AbpAuthorize]
    public class ProfileAppService : BeiDreamAbpAppServiceBase, IProfileAppService
    {
        [DisableAuditing]
        public async Task ChangePassword(ChangePasswordInput input)
        {
            var user = await GetCurrentUserAsync();
            CheckErrors(await UserManager.ChangePasswordAsync(user.Id, input.CurrentPassword, input.NewPassword));
        } 
    }
}