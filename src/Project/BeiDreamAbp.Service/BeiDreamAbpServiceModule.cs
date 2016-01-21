using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using BeiDreamAbp.Domain;

namespace BeiDreamAbp.Service
{
    [DependsOn(typeof(BeiDreamAbpDomainModule), typeof(AbpAutoMapperModule))]
    public class BeiDreamAbpServiceModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            DtoMappings.Map();
        }
    }
}