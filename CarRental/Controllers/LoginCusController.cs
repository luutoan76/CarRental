using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Models;
namespace CarRental.Controllers
{
    public class LoginCus : Controller
    {
        private CarRentContext _context;
        public LoginCus(CarRentContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Customer cus)
        {
            var result = _context.Customers.Where(a => a.TenKhach.Equals(cus.TenKhach) && a.Pass.Equals(cus.Pass)).FirstOrDefault();

            if (result != null)
            {
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                return Index();
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Register(Customer cus, string repass)
        {
            var khach = _context.Customers.FirstOrDefault(k => k.TenKhach == cus.TenKhach);
            if (repass != cus.Pass)
            {
                ViewBag.TenError2 = "Mật khẩu và repass không trùng";
            }
            if (khach != null)
            {
                ModelState.AddModelError(string.Empty, "");
                ViewBag.TenError = "Tên đăng nhập đã tồn tại";
            }
            if (ModelState.IsValid)
            {
                _context.Add(cus);
                _context.SaveChanges();
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index", "LoginCus");


        }

    }
}
