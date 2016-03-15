using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Threading;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using BeiDreamAbp.Presentation.Models.Home;
using BeiDreamAbp.Service.Security;
using BeiDreamAbp.Service.Tasks;
using BeiDreamAbp.Service.Tasks.Dtos;

namespace BeiDreamAbp.Presentation.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : BeiDreamAbpControllerBase
    {
        private readonly ITaskService _taskService;
        public HomeController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: Home
        public async Task<ActionResult> Index()
        {
            var tasks = _taskService.GetTasks(new GetTasksInput(){Name="AAAA"});
            await _taskService.CreateOrUpdateTasks(new GetTasksInput(){Name="AAAA"});
            return View(tasks);
        }
    }
}