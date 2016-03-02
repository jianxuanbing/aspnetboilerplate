using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Threading;
using Abp.Web.Mvc.Authorization;
using BeiDreamAbp.Presentation.Models.Home;
using BeiDreamAbp.Service.Security;
using BeiDreamAbp.Service.Tasks;
using BeiDreamAbp.Service.Tasks.Dtos;

namespace BeiDreamAbp.Presentation.Controllers
{
    [AbpMvcAuthorize]
    public class LayoutController : BeiDreamAbpControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly ISecurityAppService _securityAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        public LayoutController(IUserNavigationManager userNavigationManager, ILocalizationManager localizationManager, ISecurityAppService securityAppService, IMultiTenancyConfig multiTenancyConfig)
        {
            _userNavigationManager = userNavigationManager;
            _localizationManager = localizationManager;
            _securityAppService = securityAppService;
            _multiTenancyConfig = multiTenancyConfig;
        }

        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Sidebar(string currentPageName = "")
        {
            var sidebarModel = new SidebarViewModel
            {
                Menu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.UserId)),
                CurrentPageName = currentPageName
            };

            return PartialView("_Sidebar", sidebarModel);
        }
        [ChildActionOnly]
        public PartialViewResult LanguageSelection()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _localizationManager.CurrentLanguage,
                Languages = _localizationManager.GetAllLanguages()
            };

            return PartialView("_LanguageSelection", model);
        }

        [ChildActionOnly]
        public PartialViewResult UserMenuOrLoginLink()
        {
            UserMenuOrLoginLinkViewModel model;

            if (AbpSession.UserId.HasValue)
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    LoginInformations = AsyncHelper.RunSync(() => _securityAppService.GetCurrentLoginInformations()),
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                };
            }
            else
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled
                };
            }

            return PartialView("_UserMenuOrLoginLink", model);
        }
    }
}