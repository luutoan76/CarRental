using CarRental.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            var car = _context.Xes.ToList();
            return View(car);
        }

        // cái code này là chức năng cho bên khách hàng nha ae
    }
}
