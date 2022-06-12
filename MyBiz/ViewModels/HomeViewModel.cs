using MyBiz.Models;
using System.Collections.Generic;

namespace MyBiz.ViewModels
{
    public class HomeViewModel
    {
        public List<TeamMember> TeamMembers { get; set; }
        public List<Service> Services { get; set; }
        public List<MainSlider> MainSliders { get; set; }
    }
}
