using System.Reflection;
using Abp.Dependency;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Zero;
using Abp.Zero.Configuration;
using BeiDreamAbp.Domain.Authorization;
using BeiDreamAbp.Domain.Authorization.Roles;
using BeiDreamAbp.Domain.Features;

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
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    BeiDreamAbpConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "BeiDreamAbp.Domain.Localization.Source"
                        )
                    )
                );
            //初始化版本的特性集合到配置集合
            Configuration.Features.Providers.Add<BeiDreamAbpFeatureProvider>();
            //设置开启多租户
            Configuration.MultiTenancy.IsEnabled = true;

            //些角色管理配置
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            //初始化权限集合到配置集合
            Configuration.Authorization.Providers.Add<BeiDreamAbpAuthorizationProvider>();

#if DEBUG
            //Disabling email sending in debug mode(禁止邮件发送,通过注册空邮件发送对象实现)
            IocManager.Register<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
#endif
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