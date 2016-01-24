using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using BeiDreamAbp.Domain;
using Microsoft.AspNet.Identity;

namespace BeiDreamAbp.Presentation.Controllers
{
    public class BeiDreamAbpControllerBase : AbpController
    {
        protected BeiDreamAbpControllerBase()
        {
            //LocalizationSourceName = BeiDreamAbpConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            //Abp.Zero里的扩展方法
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}