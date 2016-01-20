using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using BeiDreamAbp.Domain;

namespace BeiDreamAbp.Infrastructure.Ef
{
    /// <summary>
    /// 基础设施层模块注册,继承AbpModule抽象类,依赖于BeiDreamAbpDomainModule和AbpEntityFrameworkModule这两个模块
    /// </summary>
    [DependsOn(typeof(BeiDreamAbpDomainModule), typeof(AbpEntityFrameworkModule))]
    public class BeiDreamAbpInfrastructureEfModule : AbpModule
    {
        /// <summary>
        /// 重写AbpModule的初始化方法代码,注册当前程序集的接口实现
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}