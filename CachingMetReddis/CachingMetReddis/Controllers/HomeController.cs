using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CachingMetReddis.Models;
using CachingMetRedis.Models;

namespace CachingMetReddis.Controllers
{
    public class HomeController : Controller
    {
        private readonly AirBNBRedisContext _airBNBRedisContext;

        public HomeController(AirBNBRedisContext airBNBRedisContext)
        {
            _airBNBRedisContext = airBNBRedisContext;
        }

        public IActionResult Index()
        {
            return Ok(_airBNBRedisContext.SummaryListings);
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
