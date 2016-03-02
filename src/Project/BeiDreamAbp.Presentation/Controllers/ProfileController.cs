using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace BeiDreamAbp.Presentation.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        public PartialViewResult ChangePasswordModal()
        {
            return PartialView("_ChangePasswordModal");
        }
    }
}