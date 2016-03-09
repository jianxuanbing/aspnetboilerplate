using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using BeiDreamAbp.Domain.Authorization;
using BeiDreamAbp.Presentation.Models.Languages;
using BeiDreamAbp.Service.Localization;
using BeiDreamAbp.Service.Localization.Dto;

namespace BeiDreamAbp.Presentation.Controllers
{
    [AbpMvcAuthorize(PermissionNames.SystemsManagePages_Administration_Languages)]
    public class LanguagesController : BeiDreamAbpControllerBase
    {
        private readonly ILanguageAppService _languageAppService;

        public LanguagesController(ILanguageAppService languageAppService)
        {
            _languageAppService = languageAppService;
        }

        // GET: Languages
        public ActionResult Index()
        {
            return View();
        }
        [WrapResult(false)]   //添加此标注，则返回未经过封装的json数据
        public async Task<JsonResult> GetLanguages(int limit, int offset, string name, string displayName)
        {
            var languages = await _languageAppService.GetLanguages();
            var total = languages.Items.Count;
            var rows = languages.Items.Skip(offset).Take(limit).Where(p => p.Name.Contains(name) && p.DisplayName.Contains(displayName)).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }
        public async Task<PartialViewResult> CreateOrEditModal(int? id)
        {
            var output = await _languageAppService.GetLanguageForEdit(new NullableIdInput { Id = id });
            var viewModel = new CreateOrEditLanguageModalViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
        [HttpPost]
        public async Task<JsonResult> CreateOrUpdateLanguage(CreateOrUpdateLanguageInput createOrUpdateLanguageInput)
        {
            await _languageAppService.CreateOrUpdateLanguage(createOrUpdateLanguageInput);
            return Json(new MvcAjaxResponse { Result = "操作成功!" });
        }
        [HttpPost]
        public async Task<JsonResult> DeleteLanguage(IdInput input)
        {
            //todo 实现关闭软删除功能，直接数据库删除掉数据(本身已关闭多租户过滤器，如何同时关闭软删除过滤器？)
            await _languageAppService.DeleteLanguage(input);
            return Json(new MvcAjaxResponse { Result = "删除成功!" });
        }
        [HttpPost]
        public async Task<JsonResult> SetDefaultLanguage(IdInput input)
        {
            await _languageAppService.SetDefaultLanguage(input);
            return Json(new MvcAjaxResponse { Result = "设置成功!" });
        }
    }
}