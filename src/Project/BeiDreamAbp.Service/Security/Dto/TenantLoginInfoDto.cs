using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BeiDreamAbp.Domain.MultiTenancy;

namespace BeiDreamAbp.Service.Security.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}