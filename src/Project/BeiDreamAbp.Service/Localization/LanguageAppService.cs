using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Localization;
using BeiDreamAbp.Domain.Authorization;
using BeiDreamAbp.Service.Localization.Dto;

namespace BeiDreamAbp.Service.Localization
{
    [AbpAuthorize(PermissionNames.SystemsManagePages_Administration_Languages)]
    public class LanguageAppService : BeiDreamAbpAppServiceBase, ILanguageAppService
    {
        private readonly IApplicationLanguageManager _applicationLanguageManager;

        public LanguageAppService(IApplicationLanguageManager applicationLanguageManager)
        {
            _applicationLanguageManager = applicationLanguageManager;
        }

        public async Task<GetLanguagesOutput> GetLanguages()
        {
            var languages = (await _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId)).OrderBy(l => l.DisplayName);
            var defaultLanguage = await _applicationLanguageManager.GetDefaultLanguageOrNullAsync(AbpSession.TenantId);

            return new GetLanguagesOutput(
                languages.MapTo<List<ApplicationLanguageListDto>>(),
                defaultLanguage == null ? null : defaultLanguage.Name
                );
        }
    }
}