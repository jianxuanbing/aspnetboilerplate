﻿using Abp.Application.Services.Dto;

namespace BeiDreamAbp.Service.Localization.Dto
{
    public class LanguageTextListDto : IDto
    {
        public string Key { get; set; }

        public string BaseValue { get; set; }

        public string TargetValue { get; set; }
    }
}