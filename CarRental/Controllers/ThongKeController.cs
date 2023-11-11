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
        // GET: Customers
        public async Task<IActionResult> Index()
        {


            return View(await _context.ThanhToans.ToListAsync());

		}


	}
}
