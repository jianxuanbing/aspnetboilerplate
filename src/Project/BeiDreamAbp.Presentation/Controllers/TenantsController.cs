using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Auditing;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using BeiDreamAbp.Domain.Authorization;
using BeiDreamAbp.Presentation.Models.Profile;
using BeiDreamAbp.Service.Authorization.Users.Profile.Dto;

namespace BeiDreamAbp.Presentation.Controllers
{
    [AbpMvcAuthorize(PermissionNames.SystemsManagePages_Tenants)]
    public class TenantsController : BeiDreamAbpControllerBase
    {
        // GET: Tenants
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult AddTenantModal()    
        {
            return PartialView("_AddTenantModal");
        }
        [HttpPost]
        [DisableAuditing]
        public JsonResult AddTenant(ChangePasswordViewModel changePasswordViewModel)
        {
            return Json(new MvcAjaxResponse { Result = "租户添加成功!" });
        }
    }
}