using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiz.DAL;
using MyBiz.Models;
using System.Linq;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("manage")]
    public class PositionController : Controller
    {
        private AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var positions = _context.Positions.Include(x=>x.TeamMembers).ToList();
            return View(positions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Position position)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(!_context.Positions.Any(x=>x.Name == position.Name))
            {
                _context.Positions.Add(position);
            }
            else
            {
                ModelState.AddModelError("Name","Position name is already exist!");
                return View();
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Position position = _context.Positions.FirstOrDefault(x => x.Id == id);

            if(position == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            return View(position);
        }

        [HttpPost]
        public IActionResult Edit(Position position)
        {
            Position existPos = _context.Positions.FirstOrDefault(x => x.Id == position.Id);

            if(existPos == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            existPos.Name = position.Name;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Position position = _context.Positions.FirstOrDefault(x => x.Id == id);

            if (position == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            return View(position);
        }

        [HttpPost]
        public IActionResult Delete(Position position)
        {
            Position existPos = _context.Positions.FirstOrDefault(x => x.Id == position.Id);

            if( existPos == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            _context.Positions.Remove(existPos);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
