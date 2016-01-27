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
            LocalizationSourceName = BeiDreamAbpConsts.LocalizationSourceName;
            SettingManager = IocManager.Instance.Resolve<ISettingManager>();
        }
    }
}