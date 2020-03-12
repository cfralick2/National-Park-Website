using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Capstone.Web.Extensions;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAO parkDAO;

        public HomeController(IParkDAO dao)
        {
            this.parkDAO = dao;
        }

        public IActionResult Index()
        {
             var park = parkDAO.GetAllParks();
            return View("Index", park);
        }
        public IActionResult Details(string parkCode)
        {
            var park = parkDAO.GetParkDetails(parkCode);
            
            park.AddFiveDayForecast(parkDAO.FiveDayForecast(parkCode));
            bool farenheit = HttpContext.Session.Get<bool>("isF");

            if (HttpContext.Session.Keys.Contains("isF") == false)
            {
                HttpContext.Session.Set("isF", true);
            }
            foreach (Weather weather in park.Forecast)
            {
                weather.Farenheit = HttpContext.Session.Get<bool>("isF");
            }
          
            return View(park);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult ChangeTemp(string parkCode)
        {
            bool farenheit = HttpContext.Session.Get<bool>("isF");
            HttpContext.Session.Set("isF", !farenheit);
            // get bool, set session, 
            return RedirectToAction("Details", "Home", new { parkcode = parkCode });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
