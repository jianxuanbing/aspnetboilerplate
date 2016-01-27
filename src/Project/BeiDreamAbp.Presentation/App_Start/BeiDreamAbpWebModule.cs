using System.Reflection;
using Abp.Modules;
using Abp.Zero.Configuration;
using BeiDreamAbp.Infrastructure.Ef;

namespace BeiDreamAbp.Presentation
{
     [DependsOn(typeof(BeiDreamAbpInfrastructureEfModule))]
    public class BeiDreamAbpWebModule : AbpModule
    {
         public override void PreInitialize()
         {
             //Use database as language management
             //Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

             //Configure navigation/menu
             Configuration.Navigation.Providers.Add<BeiDreamAbpNavigationProvider>();
         }

         public override void Initialize()
         {
             IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

         }
    }
}