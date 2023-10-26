using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly CarRentContext _context;

        public CheckoutController(CarRentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Order(DatXe datXe)
        {
            if (ModelState.IsValid)
            {
                _context.DatXes.Add(datXe);
                _context.SaveChanges();

                // Hiển thông báo thuê thành công
                TempData["SuccessMessage"] = "Bạn đã thuê xe thành công!";

                // Quay về trang dashboard
                return RedirectToAction("Index", "Main");
            }

            return RedirectToAction("Index", "Main");
		}
    }
}
