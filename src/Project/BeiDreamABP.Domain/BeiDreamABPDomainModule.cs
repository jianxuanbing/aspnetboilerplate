using System.Reflection;
using Abp.Modules;

namespace BeiDreamAbp.Domain
{
    /// <summary>
    /// 领域层模块注册,继承AbpModule抽象类
    /// </summary>
    public class BeiDreamAbpDomainModule : AbpModule
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