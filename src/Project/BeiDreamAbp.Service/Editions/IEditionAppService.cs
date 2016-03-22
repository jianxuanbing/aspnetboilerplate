using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BeiDreamAbp.Service.Editions.Dto;

namespace BeiDreamAbp.Service.Editions
{
    public interface IEditionAppService : IApplicationService
    {
        Task<ListResultOutput<EditionListDto>> GetEditions();
        Task<GetEditionForEditOutput> GetEditionForEdit(NullableIdInput input);
    }
}