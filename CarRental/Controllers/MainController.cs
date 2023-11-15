using CarRental.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using X.PagedList;

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
        public async Task<IActionResult> Index(string searchString, int? to, int? from, int? pageNumber)
        {
            string tenkhach = Request.Cookies["tenkhach"];
            ViewBag.tenkhach = tenkhach;
            //var pageNumber = page ?? 1;
            var car = from m in _context.Xes select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (to != null && from != null)
                {
                    car = car.Where(s => s.Ten!.Contains(searchString) && s.Gia >= to && s.Gia <= from);
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
                    car = car.Where(s => s.Ten!.Contains(searchString) && s.Gia >= to && s.Gia <= from);
                }
            }
            if (searchString != null)
            {
                pageNumber = 1;
            }
            int pageSize =6;
            return View(await PaginatedList<Xe>.CreateAsync(car.AsNoTracking(), pageNumber ?? 1, pageSize));

            //if (page == null)
            //{
            //    page = 1;
            //}
            //if (pageSize == null)
            //{
            //    pageSize = 10;
            //}
            //ViewBag.PageSize = pageSize;
            //var cars = _context.Xes.ToList();
            //ViewBag.Xes = _context.Xes.ToList().ToPagedList(pageNumber, 1);
            //var pagedList = await car.ToPagedListAsync(page, pageSize);
            //return View();
            //return View(pagedList);
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
            string tenkhach = Request.Cookies["tenkhach"];
            ViewBag.tenkhach = tenkhach;
            if (id == null)
			{
				return NotFound();
			}

            var xe = await _context.Xes
                .Include(x => x.TenLoaiNavigation)
                .FirstOrDefaultAsync(m => m.BienSo == id);
            string trangthai;
            if (xe.TrangThai.ToString() == "Đang thuê")
            {
                TempData["ThonBao"] = "Xe đã được thuê vui lòng chọn xe khác";
                return RedirectToAction("Index");
            }
             ViewBag.Xe = xe;
            if (xe == null)
            {
                return NotFound();
            }

            return View();
        }
        // cái code này là chức năng cho bên khách hàng nha ae

        public IActionResult History()
        {

            string tenkhach = Request.Cookies["tenkhach"];
            ViewBag.tenkhach = tenkhach;
            var info = _context.DatXes.Where(a => a.TenKhach == tenkhach).ToList();
            var bienso = info.Select(a => a.BienSo).ToList();
            List<string> dsbienso = new List<string>();
            IList<History> list = new List<History>();
            foreach (string item in bienso)
            {
                dsbienso.Add(item);
            }
            
            foreach (string item in dsbienso)
            {
                var tenxe = _context.Xes.Where(a => a.BienSo == item).FirstOrDefaultAsync();
                DateTime ngay1 = (DateTime)info.FirstOrDefault(a => a.BienSo == item).NgayDat;
                DateTime ngay2 = (DateTime)info.FirstOrDefault(a => a.BienSo == item).NgayTra;
                int price = tenxe.Result.Gia;
                TimeSpan distance = ngay2.Subtract(ngay1);
                int day = ngay2.Subtract(ngay1).Days;
                int total = price * day;
                string trangthai;
                if (ngay2.Day < DateTime.Now.Day)
                {
                    trangthai = "Đã trả";
                }
                else
                {
                    trangthai = "Đang thuê";
                }
                list.Add(new History() { Ten = tenxe.Result.Ten, Gia = total, Hinh = tenxe.Result.Hinh , ngaydat = info.FirstOrDefault(a => a.BienSo == item).NgayDat, ngaytra = info.FirstOrDefault(a => a.BienSo == item).NgayTra , TrangThai = trangthai});

            }
            
            ViewData["list"] = list;
            

            return View(info);
        }
    }
}
