using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using BeiDreamAbp.Domain.Authorization;
using BeiDreamAbp.Domain.Authorization.Roles;
using BeiDreamAbp.Domain.Authorization.Users;
using BeiDreamAbp.Domain.Editions;
using BeiDreamAbp.Domain.MultiTenancy;
using BeiDreamAbp.Infrastructure.Ef.EntityFramework;

namespace BeiDreamAbp.Infrastructure.Ef.Migrations.SeedData
{
    public class DefaultTenantRoleAndUserCreator
    {
        private readonly BeiDreamAbpDbContext _context;

        public DefaultTenantRoleAndUserCreator(BeiDreamAbpDbContext context)
        {
            _context = context;
        }
        public void Create()
        {
            CreateHostAndUsers();
            CreateDefaultTenantAndUsers();
        }

        private void CreateHostAndUsers()
        {
            //添加超级角儿,无租户限制，不能删除和修改
            var adminRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.Admin, StaticRoleNames.Host.Admin) { IsStatic = true, IsDefault = true });
                _context.SaveChanges();
            }
            //添加超级管理员用户,无租户限制，不能删除和修改
            var adminUserForHost = _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == User.AdminUserName);
            if (adminUserForHost == null)
            {
                adminUserForHost = _context.Users.Add(
                    new User
                    {
                        TenantId = null,
                        UserName = User.AdminUserName,
                        Name = "admin",
                        Surname = "admin",
                        EmailAddress = "admin@aspnetzero.com",
                        IsEmailConfirmed = true,
                        ShouldChangePasswordOnNextLogin = true,
                        IsActive = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                _context.SaveChanges();

                //给超级管理员用户设置超级角色
                _context.UserRoles.Add(new UserRole(adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();

                //授予超级角色所有权限
                var permissions = PermissionFinder
                    .GetAllPermissions(new BeiDreamAbpAuthorizationProvider())//通过权限查找器获取硬编码在程序集的类AppAuthorizationProvider里配置的权限
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))  //过滤只有租赁方的权限集合(也就是超级管理员)
                    .ToList();

                //给超级用户添加获取到的权限集合
                foreach (var permission in permissions)
                {
                    if (!permission.IsGrantedByDefault)  //如果此权限不是默认授权给用户(暂时没明白作用)
                    {
                        _context.Permissions.Add(
                            new RolePermissionSetting
                            {
                                Name = permission.Name,
                                IsGranted = true,
                                RoleId = adminRoleForHost.Id   //租赁方角色Id（超级角色）
                            });
                    }
                }

                _context.SaveChanges();
            }
        }
        private void CreateDefaultTenantAndUsers()
        {
            //添加默认的租户，租户名称为Default(唯一性)
            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                defaultTenant = new Tenant(Tenant.DefaultTenantName, Tenant.DefaultTenantDisplayName);

                //给默认租户添加默认系统套餐号(标准版套餐)
                var defaultEdition = _context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                defaultTenant = _context.Tenants.Add(defaultTenant);
                _context.SaveChanges();
            }

            //给默认的租户添加一个管理员admin角色
            var adminRoleForDefaultTenant = _context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRoleForDefaultTenant == null)
            {
                adminRoleForDefaultTenant = _context.Roles.Add(new Role(defaultTenant.Id, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true });
                _context.SaveChanges();
            }
            //给默认的租户添加一个user角色
            var userRoleForDefaultTenant = _context.Roles.FirstOrDefault(r => r.TenantId == defaultTenant.Id && r.Name == StaticRoleNames.Tenants.User);
            if (userRoleForDefaultTenant == null)
            {
                _context.Roles.Add(new Role(defaultTenant.Id, StaticRoleNames.Tenants.User, StaticRoleNames.Tenants.User) { IsStatic = true, IsDefault = true });
                _context.SaveChanges();
            }

            //给默认租户添加一个admin用户，此用户拥有默认租户的admin角色
            var adminUserForDefaultTenant = _context.Users.FirstOrDefault(u => u.TenantId == defaultTenant.Id && u.UserName == User.AdminUserName);
            if (adminUserForDefaultTenant == null)
            {
                adminUserForDefaultTenant = User.CreateTenantAdminUser(defaultTenant.Id, "admin@defaulttenant.com",
                    "123qwe");
                adminUserForDefaultTenant.IsEmailConfirmed = true;
                adminUserForDefaultTenant.ShouldChangePasswordOnNextLogin = true;
                adminUserForDefaultTenant.IsActive = true;

                _context.Users.Add(adminUserForDefaultTenant);
                _context.SaveChanges();

                //给此用户添加默认租户的admin角色
                _context.UserRoles.Add(new UserRole(adminUserForDefaultTenant.Id, adminRoleForDefaultTenant.Id));
                _context.SaveChanges();
            }

            //给默认租户管理员添加所有权限
            var permissions = PermissionFinder
                .GetAllPermissions(new BeiDreamAbpAuthorizationProvider())//通过权限查找器获取硬编码在程序集的类AppAuthorizationProvider里配置的权限
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))//过滤只有租户方的权限集合
                .ToList();

            foreach (var permission in permissions)
            {
                if (!permission.IsGrantedByDefault)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRoleForDefaultTenant.Id  //默认租户admin角色ID
                        });
                }
            }

            _context.SaveChanges();
        }

    }
}