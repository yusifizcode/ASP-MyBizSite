using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiz.DAL;
using MyBiz.Helpers;
using MyBiz.Models;
using System.Linq;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("manage")]
    public class TeamMemberController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public TeamMemberController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var teamMembers = _context.TeamMembers.Include(x=>x.Position).ToList();
            return View(teamMembers);
        }

        public IActionResult Create()
        {
            ViewBag.Positions = _context.Positions.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(TeamMember member)
        {

            if(!_context.Positions.Any(x=>x.Id == member.PositionId))
            {
                ModelState.AddModelError("PositionId", "Position not found!");
                ViewBag.Positions = _context.Positions.ToList();
                return View();
            }

            if(member.ImageFile != null)
            {
                if (member.ImageFile.ContentType != "image/png" && member.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Image must be png, jpg or jpeg!");
                }

                if (member.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image must be less than 2MB!");
                }
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Image is reqired!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Positions = _context.Positions.ToList();
                return View();
            }

            member.Image = FileManager.Save(_env.WebRootPath,"uploads/team",member.ImageFile);



            _context.TeamMembers.Add(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            TeamMember member = _context.TeamMembers.FirstOrDefault(x => x.Id == id);

            if (member == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            ViewBag.Positions = _context.Positions.ToList();
            return View(member);
        }

        [HttpPost]
        public IActionResult Edit(TeamMember member)
        {
            TeamMember existMember = _context.TeamMembers.FirstOrDefault(x=>x.Id == member.Id);

            if (member == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            if(member.ImageFile != null)
            {
                if (member.ImageFile.ContentType != "image/png" && member.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be png, jpg or jpeg!");
                }

                if(member.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be less than 2MB!");
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Positions = _context.Positions.ToList();
                    return View();
                }

                string newFileName = FileManager.Save(_env.WebRootPath,"uploads/team",member.ImageFile);

                FileManager.Delete(_env.WebRootPath, "uploads/team", existMember.Image);

                existMember.Image = newFileName;

            }
            else
            {
                ModelState.AddModelError("ImageFile", "Image is required!");
            }

            existMember.Fullname = member.Fullname;
            existMember.Position = member.Position;
            existMember.Desc = member.Desc;

            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            TeamMember member = _context.TeamMembers.Include(x=>x.Position).FirstOrDefault(x => x.Id == id);

            if(member == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            return View(member);
        }

        [HttpPost]
        public IActionResult Delete(TeamMember teamMember)
        {
            TeamMember member = _context.TeamMembers.FirstOrDefault(x=>x.Id == teamMember.Id);
            if(member == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            FileManager.Delete(_env.WebRootPath,"uploads/team",member.Image);

            _context.TeamMembers.Remove(member);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
