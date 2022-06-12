using Microsoft.AspNetCore.Mvc;
using MyBiz.DAL;
using MyBiz.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ServiceController : Controller
    {
        private AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Service> services = _context.Services.ToList();
            return View(services);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {

            Service service = _context.Services.FirstOrDefault(x=>x.Id == id);

            if(service == null)
            {
                return RedirectToAction("error","dashboard");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            return View(service);
        }

        [HttpPost]
        public IActionResult Edit(Service service)
        {
            Service existService = _context.Services.FirstOrDefault(x => x.Id == service.Id);

            if (existService == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            existService.Title = service.Title;
            existService.Desc = service.Desc;
            existService.Icon = service.Icon;

            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Service service = _context.Services.FirstOrDefault(x=>x.Id == id);

            if(service == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            return View(service);
        }

        [HttpPost]
        public IActionResult Delete(Service service)
        {
            Service existService = _context.Services.FirstOrDefault(x=>x.Id == service.Id);

            if(existService == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            _context.Services.Remove(existService);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
