using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace CarRental.Controllers
{
    public class XesController : Controller
    {
        private readonly ILogger<XesController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly CarRentContext _context;

        public XesController(CarRentContext context, IWebHostEnvironment hostEnvironment , ILogger<XesController> logger)
        {
            _logger = logger;
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Xes
        public async Task<IActionResult> Index()
        {

            var carRentContext = _context.Xes.Include(x => x.TenLoaiNavigation);
            return View(await carRentContext.ToListAsync());
        }
        public async Task<IActionResult> IndexXe()
        {
            var carRentContext = _context.Xes.Include(x => x.TenLoaiNavigation);
            return View(await carRentContext.ToListAsync());
        }

        // GET: Xes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes
                .Include(x => x.TenLoaiNavigation)
                .FirstOrDefaultAsync(m => m.BienSo == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // GET: Xes/Create
        public IActionResult Create()
        {
            ViewData["TenLoai"] = new SelectList(_context.Loaixes, "TenLoai", "TenLoai");
            return View();
        }

        // POST: Xes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BienSo,Ten,TenLoai,MoTa,Gia,TrangThai,ImageFile")] Xe xe)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = webHostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(xe.ImageFile.FileName);
                string extension = Path.GetExtension(xe.ImageFile.FileName);
                xe.Hinh = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image", filename);
                using (var fileStream = new FileStream(path , FileMode.Create))
                {
                    await xe.ImageFile.CopyToAsync(fileStream);
                }
                _context.Add(xe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TenLoai"] = new SelectList(_context.Loaixes, "TenLoai", "TenLoai", xe.TenLoai);
            return View(xe);
        }

        /*private string UploadFile(Xe xe)
        {
            string uniqueFileName = null;
            if (xe.Hinh != null)
            {
                string uploaddir = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "-" + xe.Hinh.FileName;
                string filePath = Path.Combine(uploaddir, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    xe.Hinh.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }*/
        // GET: Xes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }
            ViewData["TenLoai"] = new SelectList(_context.Loaixes, "TenLoai", "TenLoai", xe.TenLoai);
            return View(xe);
        }

        // POST: Xes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Ten,TenLoai,MoTa,Gia,TrangThai,Hinh")] Xe xe)
        {
            if (id != xe.BienSo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    string wwwRootPath = webHostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(xe.ImageFile.FileName);
                    string extension = Path.GetExtension(xe.ImageFile.FileName);
                    xe.Hinh = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image", filename);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await xe.ImageFile.CopyToAsync(fileStream);
                    }
                    _context.Update(xe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XeExists(xe.BienSo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TenLoai"] = new SelectList(_context.Loaixes, "TenLoai", "TenLoai", xe.TenLoai);
            return View(xe);
        }

        // GET: Xes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes
                .Include(x => x.TenLoaiNavigation)
                .FirstOrDefaultAsync(m => m.BienSo == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // POST: Xes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var xe = await _context.Xes.FindAsync(id);
            _context.Xes.Remove(xe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XeExists(string id)
        {
            return _context.Xes.Any(e => e.BienSo == id);
        }
    }
}
