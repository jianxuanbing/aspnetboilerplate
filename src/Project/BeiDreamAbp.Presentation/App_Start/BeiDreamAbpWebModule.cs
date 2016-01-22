using System.Reflection;
using Abp.Modules;
using BeiDreamAbp.Infrastructure.Ef;

namespace BeiDreamAbp.Presentation
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