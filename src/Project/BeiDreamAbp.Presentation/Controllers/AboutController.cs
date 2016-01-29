using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace BeiDreamAbp.Presentation.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : BeiDreamAbpControllerBase
    {
        // GET: About
        public ActionResult Index()
        {
            var about = L("About");
            ViewBag.Title = about;
            return View();
        }
    }
}