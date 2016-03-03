using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using System.Threading.Tasks;
using Abp.Web.Mvc.Models;
using BeiDreamAbp.Presentation.Models.Profile;
using BeiDreamAbp.Service.Authorization.Users.Profile;
using BeiDreamAbp.Service.Authorization.Users.Profile.Dto;

namespace BeiDreamAbp.Presentation.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : BeiDreamAbpControllerBase
    {
        private readonly IProfileAppService _profileAppService;

        public ProfileController(IProfileAppService profileAppService)
        {
            _profileAppService = profileAppService;
        }

        // GET: Profile
        public PartialViewResult ChangePasswordModal()
        {
            return PartialView("_ChangePasswordModal");
        }
        [HttpPost]
        //[DisableAuditing]
        public async Task<JsonResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            await _profileAppService.ChangePassword(new ChangePasswordInput() {CurrentPassword = changePasswordViewModel.CurrentPassword,NewPassword= changePasswordViewModel.NewPassword });
            return Json(new MvcAjaxResponse { Result="密码修改成功!" });
        }
    }
}