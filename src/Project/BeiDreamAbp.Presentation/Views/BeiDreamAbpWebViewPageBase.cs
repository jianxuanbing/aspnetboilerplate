using Abp.Configuration;
using Abp.Dependency;
using Abp.Web.Mvc.Views;
using BeiDreamAbp.Domain;

namespace BeiDreamAbp.Presentation.Views
{
    public abstract class BeiDreamAbpWebViewPageBase : BeiDreamAbpWebViewPageBase<dynamic>
    {

    }

    public abstract class BeiDreamAbpWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        public ISettingManager SettingManager { get; set; }

        protected BeiDreamAbpWebViewPageBase()
        {
            //本地化资源文件标识设置为当前项目的本地化资源文件标识，则会相应的读取当前项目的本地化资源标示
            LocalizationSourceName = BeiDreamAbpConsts.LocalizationSourceName;
            SettingManager = IocManager.Instance.Resolve<ISettingManager>();
        }
    }
}