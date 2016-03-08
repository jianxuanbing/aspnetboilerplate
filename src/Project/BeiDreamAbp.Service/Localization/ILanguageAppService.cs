using System.Threading.Tasks;
using Abp.Application.Services;
using BeiDreamAbp.Service.Localization.Dto;

namespace BeiDreamAbp.Service.Localization
{
    public interface ILanguageAppService : IApplicationService
    {
        Task<GetLanguagesOutput> GetLanguages();
    }
}