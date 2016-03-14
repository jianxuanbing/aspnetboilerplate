using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Localization;
using Abp.Extensions;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using BeiDreamAbp.Domain;
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
        private readonly ILanguageManager _languageManager;
        private readonly IApplicationLanguageTextManager _applicationLanguageTextManager;
        public LanguagesController(ILanguageAppService languageAppService, ILanguageManager languageManager, IApplicationLanguageTextManager applicationLanguageTextManager)
        {
            _languageAppService = languageAppService;
            _languageManager = languageManager;
            _applicationLanguageTextManager = applicationLanguageTextManager;
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
        public async Task<PartialViewResult> TextModal(int? id,
            string sourceName = "",
            string baseLanguageName = "",
            string targetValueFilter = "ALL",
            string filterText = "")
        {

            if (sourceName.IsNullOrEmpty())
            {
                sourceName = BeiDreamAbpConsts.LocalizationSourceName;
            }

            if (baseLanguageName.IsNullOrEmpty())
            {
                baseLanguageName = LocalizationManager.CurrentLanguage.Name;
            }
            var languages = await _languageAppService.GetLanguages();
            //Create view model
            var viewModel = new LanguageTextsViewModel();

            viewModel.LanguageName = languages.Items.SingleOrDefault(p=>p.Id==id).Name;

            viewModel.Languages = LocalizationManager.GetAllLanguages().ToList();

            viewModel.Sources = LocalizationManager
                .GetAllSources()
                .Where(s => s.GetType() == typeof(MultiTenantLocalizationSource))
                .Select(s => new SelectListItem()
                {
                    Value = s.Name,
                    Text = s.Name,
                    Selected = s.Name == sourceName
                })
                .ToList();

            viewModel.BaseLanguageName = baseLanguageName;

            viewModel.TargetValueFilter = targetValueFilter;
            viewModel.FilterText = filterText;

            return PartialView("_TextModal", viewModel);
        }

        [WrapResult(false)] 
        public async Task<JsonResult> GetLanguageTexts(GetLanguageTextsInput input)
        {
            //LanguageTexts已缓存
            PagedResultOutput<LanguageTextListDto> tt = await _languageAppService.GetLanguageTexts(input);
            return Json(new { total = tt.TotalCount, rows = tt.Items }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult EditTextModal(
            string sourceName,
            string baseLanguageName,
            string languageName,
            string key)
        {
            var languages = _languageManager.GetLanguages();

            var baselanguage = languages.FirstOrDefault(l => l.Name == baseLanguageName);
            if (baselanguage == null)
            {
                throw new ApplicationException("Could not find language: " + baseLanguageName);
            }

            var targetLanguage = languages.FirstOrDefault(l => l.Name == languageName);
            if (targetLanguage == null)
            {
                throw new ApplicationException("Could not find language: " + languageName);
            }

            var baseText = _applicationLanguageTextManager.GetStringOrNull(
                AbpSession.TenantId,
                sourceName,
                CultureInfo.GetCultureInfo(baseLanguageName),
                key
                );

            var targetText = _applicationLanguageTextManager.GetStringOrNull(
                AbpSession.TenantId,
                sourceName,
                CultureInfo.GetCultureInfo(languageName),
                key,
                false
                );

            var viewModel = new EditTextModalViewModel
            {
                SourceName = sourceName,
                BaseLanguage = baselanguage,
                TargetLanguage = targetLanguage,
                BaseText = baseText,
                TargetText = targetText,
                Key = key
            };

            return PartialView("_EditTextModal", viewModel);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateLanguageText(UpdateLanguageTextInput input)
        {
            //当多语言文本数据发生变化时,触发MultiTenantLocalizationDictionaryCacheCleaner事件,事件执行清除多语言文本缓存数据
            await _languageAppService.UpdateLanguageText(input);
            return Json(new MvcAjaxResponse { Result = "修改成功!" });
        }
    }
}