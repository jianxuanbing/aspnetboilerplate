using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Localization;

namespace BeiDreamAbp.Presentation.Models.Languages
{
    public class LanguageTextsViewModel
    {
        public string LanguageName { get; set; }

        public List<SelectListItem> Sources { get; set; }

        public List<LanguageInfo> Languages { get; set; }

        public string BaseLanguageName { get; set; }

        public string TargetValueFilter { get; set; }

        public string FilterText { get; set; }
    }
}