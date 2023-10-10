using CarRental.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<XesController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly CarRentContext _context;

        public MainController(CarRentContext context, IWebHostEnvironment hostEnvironment, ILogger<XesController> logger)
        {
            _logger = logger;
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index(string searchString, int? to, int? from)
        {
            string tenkhach = Request.Cookies["tenkhach"];
            ViewBag.tenkhach = tenkhach;
            var car = from m in _context.Xes select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                if (to != null && from != null)
                {
                    car = car.Where(s => s.Ten!.Contains(searchString) && s.Gia>=to && s.Gia<=from);
                }
                else
                {
                    car = car.Where(s => s.Ten!.Contains(searchString));
                }

            }
            else
            {
                if (to != null && from != null)
                {
                    car = car.Where(s => s.Ten!.Contains(searchString) && s.Gia>=to && s.Gia<=from);
                }
            }
            return View(car);
        }
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
        public async Task<IActionResult> ThueXe(string id)
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
        // cái code này là chức năng cho bên khách hàng nha ae
    }
}
