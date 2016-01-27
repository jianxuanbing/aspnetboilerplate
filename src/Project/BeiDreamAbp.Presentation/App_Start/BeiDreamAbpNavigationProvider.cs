using Abp.Application.Navigation;
using Abp.Localization;
using BeiDreamAbp.Domain;
using BeiDreamAbp.Domain.Authorization;

namespace BeiDreamAbp.Presentation
{
    public class BeiDreamAbpNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "About",
                        L("About"),
                        url: "/About",
                        icon: "fa fa-info",
                        requiresAuthentication: true
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Tenants",
                        L("Tenants"),
                        url: "/Tenants",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.SystemsManagePages_Tenants
                        )
                ).AddItem(new MenuItemDefinition(
                    "Users",
                    L("Users"),
                    url: "/Users",
                    icon: "icon-users",
                    requiredPermissionName: PermissionNames.SystemsManagePages_Administration_Users
                    )
                ).AddItem(new MenuItemDefinition(
                        "Roles",
                        L("Roles"),
                        url: "/Roles",
                        icon: "icon-roles",
                        requiredPermissionName: PermissionNames.SystemsManagePages_Administration_Roles
                    )
                );
        }
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BeiDreamAbpConsts.LocalizationSourceName);
        }
    }

}