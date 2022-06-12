using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBiz.DAL;
using MyBiz.Models;
using MyBiz.ViewModels;
//using MyBiz.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyBiz.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                TeamMembers = _context.TeamMembers.Include(x=>x.Position).ToList(),
                Services = _context.Services.ToList(),
                MainSliders = _context.MainSliders.ToList(),
            };
            return View(homeVM);
        }

    }
}
