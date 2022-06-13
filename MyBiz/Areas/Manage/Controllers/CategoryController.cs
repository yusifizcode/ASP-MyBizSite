using Microsoft.AspNetCore.Mvc;
using MyBiz.DAL;
using MyBiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("manage")]
    public class CategoryController : Controller
    {
        private AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(_context.Categories.Any(x=>x.Name == category.Name))
            {
                ModelState.AddModelError("Name", "This category already exist!");
                return View();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x=>x.Id == id);

            if(category == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            Category existCat = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            if(existCat == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            if(_context.Categories.Any(x=>x.Name == category.Name))
            {
                ModelState.AddModelError("Name", "This category already exist!");
            }

            existCat.Name = category.Name;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if(category == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {

            if(category == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
