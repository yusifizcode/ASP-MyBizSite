using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiz.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public string Fullname { get; set; }
        public string Desc { get; set; }
        public string Image { get; set; }
        public Position Position { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
