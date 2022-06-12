using Microsoft.EntityFrameworkCore;
using MyBiz.Models;

namespace MyBiz.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }


        public DbSet<Position> Positions { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<MainSlider> MainSliders { get; set; }
    }
}
