﻿using Abp.Application.Navigation;
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
                        "Tenants",
                        L("Tenants"),
                        url: "/Tenants",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.SystemsManagePages_Tenants
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Editions",
                        L("Editions"),
                        url: "/Editions",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.SystemsManagePages_Editions
                        )
                ).AddItem(new MenuItemDefinition(
                    "Administration",
                    L("Administration"),
                    icon: "fa fa-navicon"
                    ).AddItem(new MenuItemDefinition(
                    "Users",
                    L("Users"),
                    url: "/Users",
                    icon: "fa fa-navicon",
                    requiredPermissionName: PermissionNames.SystemsManagePages_Administration_Users
                    )).AddItem(new MenuItemDefinition(
                        "Roles",
                        L("Roles"),
                        url: "/Roles",
                        icon: "fa fa-music",
                        requiredPermissionName: PermissionNames.SystemsManagePages_Administration_Roles
                    )).AddItem(new MenuItemDefinition(
                        "Languages",
                        L("Languages"),
                        url: "/Languages",
                        icon: "fa fa-music",
                        requiredPermissionName: PermissionNames.SystemsManagePages_Administration_Languages
                    ))
                )
                .AddItem(
                    new MenuItemDefinition(
                        "About",
                        L("About"),
                        url: "/About",
                        icon: "fa fa-info",
                        requiresAuthentication: true
                        )
                );
        }
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BeiDreamAbpConsts.LocalizationSourceName);
        }
    }

}