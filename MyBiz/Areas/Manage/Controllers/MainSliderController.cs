using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyBiz.DAL;
using MyBiz.Helpers;
using MyBiz.Models;
using System.Linq;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("manage")]
    public class MainSliderController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public MainSliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var mainSliders = _context.MainSliders.ToList();
            return View(mainSliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MainSlider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (slider == null)
            {
                return RedirectToAction("error","dashboard");
            }

            if(slider.ImageFile != null)
            {
                if(slider.ImageFile.ContentType == "image/png" && slider.ImageFile.ContentType == "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be png, jpg or jpeg!");
                }

                if(slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile","ImageFile must be less than 2MB!");
                }

                if (!ModelState.IsValid)
                {
                    return View();
                }

                slider.Image = FileManager.Save(_env.WebRootPath,"uploads/mainsliders",slider.ImageFile);
            }
            else
            {
                ModelState.AddModelError("ImageFile","Image is required");
            }

            _context.MainSliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            MainSlider slider = _context.MainSliders.FirstOrDefault(x => x.Id == id);

            if(slider == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            return View(slider);
        }

        [HttpPost]
        public IActionResult Edit(MainSlider slider)
        {
            MainSlider existSlider = _context.MainSliders.FirstOrDefault(x => x.Id == slider.Id);

            if(slider == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType == "image/png" && slider.ImageFile.ContentType == "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be png, jpg or jpeg!");
                }

                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be less than 2MB!");
                }

                if (!ModelState.IsValid)
                {
                    return View();
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/mainsliders", slider.ImageFile);

                FileManager.Delete(_env.WebRootPath, "uploads/mainsliders", existSlider.Image);

                existSlider.Image = newFileName;
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Image is required");
            }

            existSlider.Title = slider.Title;
            existSlider.Desc = slider.Desc;
            existSlider.BtnText = slider.BtnText;
            existSlider.BtnUrl = slider.BtnUrl;
            existSlider.Order = slider.Order;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            MainSlider slider = _context.MainSliders.FirstOrDefault(x=>x.Id == id);

            if(slider == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            
            return View(slider);
        }

        [HttpPost]
        public IActionResult Delete(MainSlider slider)
        {
            MainSlider existSldier = _context.MainSliders.FirstOrDefault(x=>x.Id == slider.Id);

            if (existSldier == null)
                return RedirectToAction("error", "dashboard");

            FileManager.Delete(_env.WebRootPath, "uploads/mainsliders", existSldier.Image);

            _context.MainSliders.Remove(existSldier);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
