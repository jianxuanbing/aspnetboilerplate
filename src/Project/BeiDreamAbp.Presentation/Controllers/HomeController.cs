using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Threading;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using BeiDreamAbp.Presentation.Models.Home;
using BeiDreamAbp.Service.Tasks;
using BeiDreamAbp.Service.Tasks.Dtos;

namespace BeiDreamAbp.Presentation.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : BeiDreamAbpControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserNavigationManager _userNavigationManager;
        public HomeController(ITaskService taskService, IUserNavigationManager userNavigationManager)
        {
            _taskService = taskService;
            _userNavigationManager = userNavigationManager;
        }

        // GET: Home
        public ActionResult Index()
        {
            var tasks = _taskService.GetTasks(new GetTasksInput() { Name = "AAAA" });
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
    }
}