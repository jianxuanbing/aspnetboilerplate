using System.Threading.Tasks;
using Abp.Application.Services;
using BeiDreamAbp.Service.Security.Dto;

namespace BeiDreamAbp.Service.Security
{
    public interface ISecurityAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
