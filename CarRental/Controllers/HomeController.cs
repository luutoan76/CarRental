using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices] CarRentContext dbContext)
        {
            ViewBag.CoutXe = dbContext.Xes.Count();
            ViewBag.CoutLoaiXe = dbContext.Loaixes.Count();
            ViewBag.CoutNhanVien = dbContext.NhanViens.Count();
            ViewBag.CoutCustomer = dbContext.Customers.Count();
            ViewBag.CoutBill = dbContext.DatXes.Count();
            ViewBag.CoutThongKe = dbContext.ThanhToans.Count();

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
