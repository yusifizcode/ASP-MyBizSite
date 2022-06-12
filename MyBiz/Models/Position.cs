using System.Collections.Generic;

namespace MyBiz.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
    }
}
