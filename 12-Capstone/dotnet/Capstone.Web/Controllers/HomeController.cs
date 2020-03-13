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
using static Capstone.Web.Models.WeatherAPIModel;
using System.Net.Http;
using Newtonsoft.Json;

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
        //public IActionResult Details(string parkCode)
        //{
        //    var park = parkDAO.GetParkDetails(parkCode);

        //    park.AddFiveDayForecast(parkDAO.FiveDayForecast(parkCode));
        //    bool farenheit = HttpContext.Session.Get<bool>("isF");

        //    if (HttpContext.Session.Keys.Contains("isF") == false)
        //    {
        //        HttpContext.Session.Set("isF", true);
        //    }
        //    foreach (Weather weather in park.Forecast)
        //    {
        //        weather.Farenheit = HttpContext.Session.Get<bool>("isF");
        //    }

        //    return View(park);
        //}
        [HttpGet]
        public async Task<ActionResult> Details(string parkCode)
        {
            List<Weather> weather = new List<Weather>();
            var park = parkDAO.GetParkDetails(parkCode);
            //park.AddFiveDayForecast(parkDAO.FiveDayForecast(parkCode));*///change to set weathers from API

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.darksky.net/forecast/cc281da8c085e79794f92e309796533f/");

                //HTTP GET
                var responseTask = client.GetAsync(park.Latitude.ToString() +","+ park.Longitude.ToString() + "?exclude=currently,minutely,hourly,alerts,flags");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    
                    string content = await result.Content.ReadAsStringAsync();
                    var root = JsonConvert.DeserializeObject<Rootobject>(content);
                    for (int i = 0; i<5; i++)
                    {
                        Weather w = new Weather();
                        w.FiveDayForecastValue = i + 1;
                        w.HighTemp = root.daily.data[i].temperatureHigh;
                        w.LowTemp = root.daily.data[i].temperatureLow;
                        w.Forecast = root.daily.data[i].icon;
                        weather.Add(w);
                    }
                    
                }
            }
            park.AddFiveDayForecast(weather);

            bool farenheit = HttpContext.Session.Get<bool>("isF");

            if (HttpContext.Session.Keys.Contains("isF") == false)
            {
                HttpContext.Session.Set("isF", true);
            }
            foreach (Weather weathers in park.Forecast)
            {
                weathers.Farenheit = HttpContext.Session.Get<bool>("isF");
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
