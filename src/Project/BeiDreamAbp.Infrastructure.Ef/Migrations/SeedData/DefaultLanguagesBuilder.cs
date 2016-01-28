using System.Collections.Generic;
using System.Linq;
using Abp.Localization;
using BeiDreamAbp.Infrastructure.Ef.EntityFramework;

namespace BeiDreamAbp.Infrastructure.Ef.Migrations.SeedData
{
    /// <summary>
    /// 默认支持语言种类初始化
    /// </summary>
    public class DefaultLanguagesBuilder
    {
        public static List<ApplicationLanguage> InitialLanguages { get; private set; }

        private readonly BeiDreamAbpDbContext _context;

        static DefaultLanguagesBuilder()
        {
            InitialLanguages = new List<ApplicationLanguage>
            {
                new ApplicationLanguage(null, "en", "English", "famfamfam-flag-gb"),
                new ApplicationLanguage(null, "zh-CN", "简体中文", "famfamfam-flag-cn"),
            };
        }

        public DefaultLanguagesBuilder(BeiDreamAbpDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialLanguages)
            {
                AddLanguageIfNotExists(language);
            }
        }

        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
            {
                return;
            }

            _context.Languages.Add(language);

            _context.SaveChanges();
        }
    }
}