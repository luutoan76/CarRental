using CarRental.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    public class ThueXeController : Controller
    {
        private readonly ILogger<XesController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly CarRentContext _context;
        public ThueXeController(CarRentContext context, IWebHostEnvironment hostEnvironment, ILogger<XesController> logger)
        {
            _logger = logger;
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index(string id)
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
        /*[HttpPost]
        public Task<IActionResult> ThueXeInfo(FormCollection fc)
        {

        }*/
    }
}
