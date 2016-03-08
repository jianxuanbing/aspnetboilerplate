using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Localization;
using BeiDreamAbp.Domain.Authorization;
using BeiDreamAbp.Service.Localization.Dto;

namespace BeiDreamAbp.Service.Localization
{
    [AbpAuthorize(PermissionNames.SystemsManagePages_Administration_Languages)]
    public class LanguageAppService : BeiDreamAbpAppServiceBase, ILanguageAppService
    {
        private readonly IApplicationLanguageManager _applicationLanguageManager;
        private readonly IRepository<ApplicationLanguage> _languageRepository;

        public LanguageAppService(IApplicationLanguageManager applicationLanguageManager, IRepository<ApplicationLanguage> languageRepository)
        {
            _applicationLanguageManager = applicationLanguageManager;
            _languageRepository = languageRepository;
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
        public async Task<GetLanguageForEditOutput> GetLanguageForEdit(NullableIdInput input)
        {
            ApplicationLanguage language = null;
            if (input.Id.HasValue)
            {
                language = await _languageRepository.GetAsync(input.Id.Value);
            }

            var output = new GetLanguageForEditOutput();

            //Language
            output.Language = language != null
                ? language.MapTo<ApplicationLanguageEditDto>()
                : new ApplicationLanguageEditDto();

            //Language names
            output.LanguageNames = CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .OrderBy(c => c.DisplayName)
                .Select(c => new ComboboxItemDto(c.Name, c.DisplayName + " (" + c.Name + ")") { IsSelected = output.Language.Name == c.Name })
                .ToList();

            //Flags
            output.Flags = FamFamFamFlagsHelper
                .FlagClassNames
                .OrderBy(f => f)
                .Select(f => new ComboboxItemDto(f, FamFamFamFlagsHelper.GetCountryCode(f)) { IsSelected = output.Language.Icon == f })
                .ToList();

            return output;
        }
    }
}