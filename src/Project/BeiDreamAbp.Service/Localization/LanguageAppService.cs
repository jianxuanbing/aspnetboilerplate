using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.UI;
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

        #region 新增或修改多语言
        public async Task CreateOrUpdateLanguage(CreateOrUpdateLanguageInput input)
        {
            if (input.Language.Id.HasValue)
            {
                await UpdateLanguageAsync(input);
            }
            else
            {
                await CreateLanguageAsync(input);
            }
        }
        protected virtual async Task CreateLanguageAsync(CreateOrUpdateLanguageInput input)
        {
            var culture = GetCultureInfoByChecking(input.Language.Name);

            await CheckLanguageIfAlreadyExists(culture.Name);

            await _applicationLanguageManager.AddAsync(
                new ApplicationLanguage(
                    AbpSession.TenantId,
                    culture.Name,
                    culture.DisplayName,
                    input.Language.Icon
                    )
                );
        }
        protected virtual async Task UpdateLanguageAsync(CreateOrUpdateLanguageInput input)
        {
            Debug.Assert(input.Language.Id != null, "input.Language.Id != null");

            var culture = GetCultureInfoByChecking(input.Language.Name);

            await CheckLanguageIfAlreadyExists(culture.Name, input.Language.Id.Value);

            var language = await _languageRepository.GetAsync(input.Language.Id.Value);

            language.Name = culture.Name;
            language.DisplayName = culture.DisplayName;
            language.Icon = input.Language.Icon;

            await _applicationLanguageManager.UpdateAsync(AbpSession.TenantId, language);
        }
        private CultureInfo GetCultureInfoByChecking(string name)
        {
            try
            {
                return CultureInfo.GetCultureInfo(name);
            }
            catch (CultureNotFoundException ex)
            {
                //Logger.Warn(ex.ToString(), ex);
                throw new UserFriendlyException(L("InvlalidLanguageCode"));
            }
        }

        private async Task CheckLanguageIfAlreadyExists(string languageName, int? expectedId = null)
        {
            var existingLanguage = (await _applicationLanguageManager.GetLanguagesAsync(AbpSession.TenantId))
                .FirstOrDefault(l => l.Name == languageName);

            if (existingLanguage == null)
            {
                return;
            }

            if (expectedId != null && existingLanguage.Id == expectedId.Value)
            {
                return;
            }

            throw new UserFriendlyException(L("ThisLanguageAlreadyExists"));
        } 
        #endregion

        public async Task DeleteLanguage(IdInput input)
        {
            var language = await _languageRepository.GetAsync(input.Id);
            await _applicationLanguageManager.RemoveAsync(AbpSession.TenantId, language.Name);
        }

        public async Task SetDefaultLanguage(IdInput input)
        {
            var language = await _languageRepository.GetAsync(input.Id);
            await _applicationLanguageManager.SetDefaultLanguageAsync(
                AbpSession.TenantId,
                GetCultureInfoByChecking(language.Name).Name
                );
        }
    }
}