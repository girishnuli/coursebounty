using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using coursebounty.Models;
using AspNetCore.Identity.LiteDB.Data;
using LiteDB;

namespace coursebounty.Controllers
{
    public class HomeController : Controller
    {
		private LiteDatabase _db { get; }

		public HomeController(LiteDbContext dbContext)
        {
			_db = dbContext.LiteDatabase;
		}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
