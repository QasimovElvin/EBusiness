using EBusiness.DAL;
using EBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EBusiness.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM VM = new HomeVM()
            {
                Blogs = _context.Blogs.Take(3).ToList(),
                Servs = _context.Servs.Take(3).ToList(),
                Sliders = _context.Sliders.Take(3).ToList(),
                Teams = _context.Teams.Take(3).ToList(),
                
            };
            return View(VM);
        }

      
    }
}