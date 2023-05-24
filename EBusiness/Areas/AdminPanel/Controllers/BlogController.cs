using EBusiness.DAL;
using EBusiness.Models;
using EBusiness.Utilities.Extensions;
using EBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Reflection.Metadata;

namespace EBusiness.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page=1,int take=3)
        {
            var services = _context.Blogs.Skip((page - 1) * take).Take(take).ToList();
            PaginateVM<Blog> paginate = new PaginateVM<Blog>()
            {
                Items = services,
                CurentPage = page,
                PageCount = PageCount(take)
            };
            return View(paginate);
        }
        private int PageCount(int take)
        {
            var BlogCount = _context.Blogs.Count();
            return (int)Math.Ceiling((decimal)BlogCount / take);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid) return View();

            if (blog.Imagefile == null)
            {
                ModelState.AddModelError("", "Image is Null");
                return View();
            }
            if (!blog.Imagefile.CheckType("Image/"))
            {
                ModelState.AddModelError("", "Type is wrong");
                return View();
            }
            if (blog.Imagefile.CheckSize(500))
            {
                ModelState.AddModelError("", "Size is Wrong");
                return View();
            }
            blog.Image = await blog.Imagefile.SaveFileAsync(_env.WebRootPath,"blog");
            await _context.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Blog blog)
        {
            Blog? exists = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == blog.Id);
            if (exists == null)
            {
                ModelState.AddModelError("", "Blog is null");
                return View();
            }
            if (blog.Imagefile != null)
            {
                if (!blog.Imagefile.CheckType("Image/"))
                {
                    ModelState.AddModelError("", "Type is wrong");
                    return View();
                }
                if (blog.Imagefile.CheckSize(500))
                {
                    ModelState.AddModelError("", "Size is Wrong");
                    return View();
                }
                string path = Path.Combine(_env.WebRootPath, "assets", exists.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                blog.Image = await blog.Imagefile.SaveFileAsync(_env.WebRootPath, "blog");
            }
            exists.Title = blog.Title;
            exists.Description = blog.Description;
            exists.Image = blog.Image;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Blog? exists = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            string path = Path.Combine(_env.WebRootPath, "assets", exists.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
             _context.Blogs.Remove(exists);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
