using Abp.Application.Navigation;

namespace BeiDreamAbp.Presentation.Models.Home
{
    public class SidebarViewModel
    {
        public UserMenu Menu { get; set; }

        public string CurrentPageName { get; set; } 
    }
}