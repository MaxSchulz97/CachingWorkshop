using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CachingMetReddis.Models;
using CachingMetRedis.Models;
using CachingMetRedis.Services;
using Newtonsoft.Json;

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
            IEnumerable<SummaryListings> summaryListings = null;

            if (CacheService.GetInstance().KeyExists("SummaryListings"))
            {
                summaryListings = JsonConvert.DeserializeObject<IEnumerable<SummaryListings>>(CacheService.GetInstance().StringGet("SummaryListings"));
            }
            else
            {
                // Add to cache
                summaryListings = _airBNBRedisContext.SummaryListings.ToList();
                // TimeSpan.<Something>(<Ammount>) sets the time that the cache will be stored on Azure
                CacheService.GetInstance().StringSet("SummaryListings", JsonConvert.SerializeObject(summaryListings), TimeSpan.FromDays(1));
            }

            return Ok(summaryListings);
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
