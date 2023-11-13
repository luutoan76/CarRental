using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using Microsoft.AspNetCore.Http;
using MailKit.Search;
using Microsoft.Extensions.Logging;

namespace CarRental.Controllers
{
	public class ThongKeController : Controller
	{
		private readonly CarRentContext _context;
        public ThongKeController(CarRentContext context)
		{

			_context = context;
		}
        //public async Task<IActionResult> Index(DateTime? ngayIn)
        //{
        //    if (ngayIn != null)
        //    {
        //        var thanhToans = await _context.ThanhToans.Where(t => t.NgayIn == ngayIn).ToListAsync();
        //        return View(thanhToans);
        //    }
        //    else
        //    {
        //        return View(await _context.ThanhToans.ToListAsync());
        //    }
        //}
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {

            // Check if both start and end dates are provided
            if (startDate.HasValue && endDate.HasValue)
            {
                // Retrieve records within the specified date range
                var thanhToans = await _context.ThanhToans
                    .Where(t => t.NgayIn >= startDate && t.NgayIn <= endDate)
                    .ToListAsync();

                return View(thanhToans);
            }

            // Retrieve all records if no date range is specified
            var thanhtoans = await _context.ThanhToans.ToListAsync();

            return View(thanhtoans);
        }


    }
}
