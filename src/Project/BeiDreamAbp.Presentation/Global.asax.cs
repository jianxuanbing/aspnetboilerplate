using System;
using System.Web.Mvc;
using System.Web.Routing;
using Abp.Web;

namespace BeiDreamAbp.Presentation
{
    public class MvcApplication : AbpWebApplication
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            base.Application_Start(sender, e);
        }
    }
}
