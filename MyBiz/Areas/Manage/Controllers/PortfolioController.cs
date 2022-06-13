using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiz.DAL;
using MyBiz.Helpers;
using MyBiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("manage")]
    public class PortfolioController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public PortfolioController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var portfolios = _context.Portfolios.Include(x=>x.Category).ToList();
            return View(portfolios);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View();
            }

            if(!_context.Categories.Any(x=>x.Id == portfolio.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "This category is not found!");
                ViewBag.Categories = _context.Categories.ToList();
                return View();
            }

            if(portfolio.PosterFile == null)
            {
                ModelState.AddModelError("PosterFile", "Poster file is required!");
                ViewBag.Categories = _context.Categories.ToList();
                return View();
            }
            else
            {
                if(portfolio.PosterFile.ContentType == "image/png" && portfolio.PosterFile.ContentType == "image/jpeg")
                {
                    ModelState.AddModelError("PosterFile", "File format must be png, jpg or jpeg!");
                    ViewBag.Categories = _context.Categories.ToList();
                    return View();
                }
                if (portfolio.PosterFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterFile", "File size must be less than 2MB!");
                    ViewBag.Categories = _context.Categories.ToList();
                    return View();
                }

                PortfolioImage portfolioImage = new PortfolioImage
                {
                    Name = FileManager.Save(_env.WebRootPath, "uploads/portfolio", portfolio.PosterFile),
                    PosterStatus = true
                };
                portfolio.PortfolioImages.Add(portfolioImage);
            }

            if(portfolio.ImageFiles != null)
            {
                foreach(var file in portfolio.ImageFiles)
                {
                    if (portfolio.PosterFile.ContentType == "image/png" && portfolio.PosterFile.ContentType == "image/jpeg")
                    {
                        ModelState.AddModelError("PosterFile", "File format must be png, jpg or jpeg!");
                        ViewBag.Categories = _context.Categories.ToList();
                        return View();
                    }
                    if (portfolio.PosterFile.Length > 2097152)
                    {
                        ModelState.AddModelError("PosterFile", "File size must be less than 2MB!");
                        ViewBag.Categories = _context.Categories.ToList();
                        return View();
                    }
                    if (!ModelState.IsValid)
                    {
                        ViewBag.Genres = _context.Categories.ToList();
                        return View();
                    }
                }

                foreach(var file in portfolio.ImageFiles)
                {
                    PortfolioImage portfolioImage = new PortfolioImage
                    {
                        Name = FileManager.Save(_env.WebRootPath, "uploads/portfolio", file),
                        PosterStatus = null
                    };

                    portfolio.PortfolioImages.Add(portfolioImage);
                }
            }

            _context.Portfolios.Add(portfolio);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Portfolio portfolio = _context.Portfolios.FirstOrDefault(x=>x.Id == id);

            if(portfolio == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(portfolio);
        }

        
    }
}
