using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using BeiDreamAbp.Domain.Authorization;
using BeiDreamAbp.Domain.Authorization.Roles;

namespace BeiDreamAbp.Domain
{
    /// <summary>
    /// 领域层模块注册,继承AbpModule抽象类
    /// </summary>
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class BeiDreamAbpDomainModule : AbpModule
    {
        public override void PreInitialize()
        {
            //项目启动的配置文件多租户是否开始
            Configuration.MultiTenancy.IsEnabled = true;
            //新增或移除当前程序集的本地资源文件信息(暂时理解)
            //Configuration.Localization.Sources.Add(
            //    new DictionaryBasedLocalizationSource(
            //        BeiDreamAbpConsts.LocalizationSourceName,
            //        new XmlEmbeddedFileLocalizationDictionaryProvider(
            //            Assembly.GetExecutingAssembly(),
            //            "BeiDream.NewModuleZero.Localization.Source"
            //            )
            //        )
            //    );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<BeiDreamAbpAuthorizationProvider>();
        }

        /// <summary>
        /// 重写AbpModule的初始化方法代码,注册当前程序集的接口实现
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}