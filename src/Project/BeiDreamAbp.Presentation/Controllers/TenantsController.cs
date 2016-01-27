﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using BeiDreamAbp.Domain.Authorization;

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
    }
}