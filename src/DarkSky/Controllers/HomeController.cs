using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DarkSky.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DarkSky.Controllers
{
    public class HomeController : Controller
    {
        Weather newWeather = new Weather();
        // GET: /<controller>/
        
        public IActionResult Index()
        {
            string defaultLocation = "Portland";
            ViewBag.Place = defaultLocation;
            ViewBag.Key = EnvironmentVariables.MapViewKey;
            return View();
        }
        [HttpPost]
        public IActionResult DrawMap(string mapLocation)
        {
            Console.WriteLine("location: " + mapLocation);
            string latlng = Weather.GetLocation(mapLocation);
            newWeather.GetTemp(latlng);
            newWeather.GetSummary(latlng);
            ViewBag.Place = mapLocation;
            ViewBag.Key = EnvironmentVariables.MapViewKey;

            return Json(latlng);
        }
        public IActionResult MapTest()
        {
            return View();
        }
    }

}
