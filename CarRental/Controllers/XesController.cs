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
using System.Runtime.ConstrainedExecution;
//using X.PagedList;

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
        public async Task<IActionResult> Index(string searchString, int? to, int? from)
        {
            var carRentContext = from m in _context.Xes select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                if (to != null && from != null)
                {
                    carRentContext = carRentContext.Where(s => s.Ten!.Contains(searchString) && s.Gia >= to && s.Gia <= from);
                }
                else
                {
                    carRentContext = carRentContext.Where(s => s.Ten!.Contains(searchString));
                }
            }
            else
            {
                if (to != null && from != null)
                {
                    carRentContext = carRentContext.Where(s => s.Ten!.Contains(searchString) && s.Gia >= to && s.Gia <= from);
                }
            }
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
                string uniquefilename = UploadFile(xe);
                var data = new Xe()
                {
                    Ten = xe.Ten,
                    BienSo = xe.BienSo,
                    TenLoai = xe.TenLoai,
                    MoTa = xe.MoTa,
                    Gia = xe.Gia,
                    TrangThai = xe.TrangThai,
                    Hinh = uniquefilename,
                };

                
                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TenLoai"] = new SelectList(_context.Loaixes, "TenLoai", "TenLoai", xe.TenLoai);
            return View(xe);
        }

        public string UploadFile(Xe xe)
        {
            string uniqueFileName = string.Empty;
            if (xe.ImageFile != null)
            {
                string uploaddir = Path.Combine(webHostEnvironment.WebRootPath, "Image");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + xe.ImageFile.FileName;
                string filePath = Path.Combine(uploaddir, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    xe.ImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        // GET: Xes/Edit/5
        public IActionResult Edit(int? id)
        {
            var xe = _context.Xes.Where(a => a.Id == id).SingleOrDefault();
            if (xe != null)
            {
                ViewData["TenLoai"] = new SelectList(_context.Loaixes, "TenLoai", "TenLoai", xe.TenLoai);
                return View(xe);
            }
            return RedirectToAction("Index");
            //var xe = await _context.Xes.FindAsync(id);
            //if (xe == null)
            //{
            //    return NotFound();
            //}
            
        }

        // POST: Xes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Xe xe)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var data = _context.Xes.Where(a => a.Id == xe.Id).SingleOrDefault();
                    string uniquename = string.Empty;
                    if (xe.ImageFile != null)
                    {
                        if (data.Hinh != null)
                        {
                            string filepath = Path.Combine(webHostEnvironment.WebRootPath, "Image", data.Hinh);
                            if (System.IO.File.Exists(filepath))
                            {
                                System.IO.File.Delete(filepath);
                            }
                        }
                        uniquename = UploadFile(xe);
                    }
                    data.Gia = xe.Gia;
                    data.Ten = xe.Ten;
                    data.TenLoai = xe.TenLoai;
                    data.MoTa = xe.MoTa;
                    data.TrangThai = xe.TrangThai;
                    if (xe.ImageFile != null)
                    {
                        data.Hinh = uniquename;
                    }
                    //_context.Xes.Update(data);
                    _context.SaveChanges();

                    /*string wwwRootPath = webHostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(xe.ImageFile.FileName);
                    string extension = Path.GetExtension(xe.ImageFile.FileName);
                    xe.Hinh = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image", filename);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await xe.ImageFile.CopyToAsync(fileStream);
                    }
                    _context.Update(xe);
                    await _context.SaveChangesAsync();*/
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TenLoai"] = new SelectList(_context.Loaixes, "TenLoai", xe.TenLoai);
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
