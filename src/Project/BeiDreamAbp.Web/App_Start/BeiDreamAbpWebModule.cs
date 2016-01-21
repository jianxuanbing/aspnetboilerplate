using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Abp.Modules;
using Abp.Web;
using BeiDreamAbp.Infrastructure.Ef;

namespace BeiDreamAbp.Web
{
     [DependsOn(typeof(BeiDreamAbpInfrastructureEfModule))]
    public class BeiDreamAbpWebModule : AbpModule
    {
         public override void Initialize()
         {
             IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

         }
    }
}