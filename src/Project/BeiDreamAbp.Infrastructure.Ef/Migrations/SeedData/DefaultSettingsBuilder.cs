using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using BeiDreamAbp.Infrastructure.Ef.EntityFramework;

namespace BeiDreamAbp.Infrastructure.Ef.Migrations.SeedData
{
    /// <summary>
    /// 网站默认配置项添加
    /// </summary>
    public class DefaultSettingsBuilder
    {
        private readonly BeiDreamAbpDbContext _context;

        public DefaultSettingsBuilder(BeiDreamAbpDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //Emailing
            //AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "admin@mydomain.com");
            //AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "mydomain.com mailer");

            //Languages(默认语言添加)
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "zh-CN");
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}