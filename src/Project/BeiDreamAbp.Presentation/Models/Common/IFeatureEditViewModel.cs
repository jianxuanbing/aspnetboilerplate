using System.Collections.Generic;
using Abp.Application.Services.Dto;
using BeiDreamAbp.Service.Editions.Dto;

namespace BeiDreamAbp.Presentation.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}