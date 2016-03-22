using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using BeiDreamAbp.Domain.Authorization;
using BeiDreamAbp.Presentation.Models.Editions;
using BeiDreamAbp.Service.Editions;

namespace BeiDreamAbp.Presentation.Controllers
{
    [AbpMvcAuthorize(PermissionNames.SystemsManagePages_Editions)]
    public class EditionsController : BeiDreamAbpControllerBase
    {
        private readonly IEditionAppService _editionAppService;

        public EditionsController(IEditionAppService editionAppService)
        {
            _editionAppService = editionAppService;
        }

        // GET: Editions
        public ActionResult Index()
        {
            return View();
        }
        [WrapResult(false)]   //添加此标注，则返回未经过封装的json数据
        public async Task<JsonResult> GetEditions()
        {
            var editions = await _editionAppService.GetEditions();

            return Json(new { total = editions.Items.Count, rows = editions.Items }, JsonRequestBehavior.AllowGet);
        }
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            var output = await _editionAppService.GetEditionForEdit(new NullableIdInput { Id = id });
            var viewModel = new CreateOrEditEditionModalViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}