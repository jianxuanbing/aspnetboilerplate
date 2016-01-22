using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Controllers;
using BeiDreamAbp.Service.Tasks;
using BeiDreamAbp.Service.Tasks.Dtos;

namespace BeiDreamAbp.Presentation.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : AbpController
    {
        private readonly ITaskService _taskService;
        public HomeController(ITaskService taskService)
        {
            //LocalizationSourceName = "SimpleTaskSystem";
            _taskService = taskService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var tasks = _taskService.GetTasks(new GetTasksInput() { Name = "AAAA" });
            return View();
        }
    }
}