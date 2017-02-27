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
            newWeather.GetWeather();
            return View();
        }
    }
}
