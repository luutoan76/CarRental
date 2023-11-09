using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
//using MailKit.Net.Smtp;
using System.Net.Mail;
using System.Net;
//using MimeKit;

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
        public IActionResult FindEmail(string name)
        {

            return View();
        }
        [HttpPost]
        public IActionResult FindEmail(Customer customer)
        {
            var name = _context.Customers.Where(a => a.TenKhach.Equals(customer.TenKhach)).FirstOrDefault();
            
            if (name != null)
            {
                int id = name.Id;
                string url = "https://localhost:44338/LoginCus/ResetPass/" + id.ToString();
                string email = name.Email;
                ViewBag.Email = "Một Email đã được gửi đến " + email + " vui lòng kiểm tra hộp thư";
                MailMessage mm = new MailMessage();
                mm.Subject = "Confirm reset pass";
                mm.IsBodyHtml = true;
                mm.Body =
                    $@"<html>
    <body>
        <h1>Đây là email xác nhận mật khẩu</h1>
        Để xác nhận mật khẩu hãy bấm vào <a href=""{url}"">đây</a>
    </body>
    </html>";
                
                mm.From = new MailAddress("toanluuyolo1234@gmail.com", "Car Rental");
                mm.To.Add(new MailAddress(email, "Customer"));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential("toanluuyolo1234@gmail.com", "ycor yoes jsjf enwc");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(mm);
            }
            else
            {
                ViewBag.Error = "Không có tên đăng nhập vui lòng đăng ký";
            }
            
            return View();
        }
        public IActionResult ResetPass(int? id)
        {
            var khachhang = _context.Customers.Where(a => a.Id == id).FirstOrDefault();
            ViewBag.TenKhach = khachhang.TenKhach;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPass(int? id,Customer editModel)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

                if (customer != null)
                {
                    customer.Pass = editModel.Pass;
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index" , "LoginCus");
        }


        
    }
}
