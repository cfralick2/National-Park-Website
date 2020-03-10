using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

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
            return View(park);
        }
        public IActionResult Contact()
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
