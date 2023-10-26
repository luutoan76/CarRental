using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Models;
using Microsoft.Extensions.Logging;

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
                Response.Cookies.Append("tenkhach", cus.TenKhach);
                return RedirectToAction("Index", "Main");
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
        [HttpGet]
        public IActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginAdmin(NhanVien employee)
        {
            var result = _context.NhanViens.Where(a => a.Ten.Equals(employee.Ten) && a.Pass.Equals(employee.Pass)).FirstOrDefault();

            if (result != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                return Index();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("tenkhach"); 
            return RedirectToAction("Index", "Main");
        }


        [HttpGet]
        public IActionResult Editprofile()
        {
            var tenKhach = Request.Cookies["tenkhach"];
            var customer = _context.Customers.FirstOrDefault(c => c.TenKhach == tenKhach);

            if (customer == null)
            {
                return NotFound();
            }

            var editModel = new Customer
            {
                TenKhach = customer.TenKhach,
                Sdt = customer.Sdt,
                Email = customer.Email,
                Tuoi = customer.Tuoi,
                Pass = customer.Pass,
                DiaChi = customer.DiaChi
            };

            return View(editModel);
        }

        [HttpPost]
        public IActionResult Editprofile(Customer editModel)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.FirstOrDefault(c => c.TenKhach == editModel.TenKhach);

                if (customer != null)
                {
                    customer.Sdt = editModel.Sdt;
                    customer.Email = editModel.Email;
                    customer.Tuoi = editModel.Tuoi;
                    customer.Pass = editModel.Pass;
                    customer.DiaChi = editModel.DiaChi;

                    _context.SaveChanges();
                    return RedirectToAction("Index", "Main");
                }
            }
            return View(editModel);
        }
        [HttpGet]
        public IActionResult Profile()
        {
            var tenKhach = Request.Cookies["tenkhach"];
            var customer = _context.Customers.FirstOrDefault(c => c.TenKhach == tenKhach);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }



    }
}
