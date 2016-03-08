using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using BeiDreamAbp.Domain.Authorization;
using BeiDreamAbp.Service.Localization;

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
            //var lstRes = new List<Department>();
            //for (var i = 0; i < 50; i++)
            //{
            //    var oModel = new Department();
            //    oModel.ID = Guid.NewGuid().ToString();
            //    oModel.Name = "销售部" + i;
            //    oModel.Level = i.ToString();
            //    oModel.Desc = "暂无描述信息";
            //    lstRes.Add(oModel);
            //}
            var languages = await _languageAppService.GetLanguages();
            var total = languages.Items.Count;
            var rows = languages.Items.Skip(offset).Take(limit).Where(p => p.Name.Contains(name) && p.DisplayName.Contains(displayName)).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }
    }
    public class Department
    {
        public string ID { set; get; }

        public string Name { set; get; }

        public string ParentName { set; get; }

        public string Level { set; get; }

        public string Desc { set; get; }
    }
}