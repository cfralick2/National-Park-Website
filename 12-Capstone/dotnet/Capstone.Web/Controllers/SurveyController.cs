using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private ISurveyDAO surveyDAO;
        public SurveyController(ISurveyDAO dao)
        {
            this.surveyDAO = dao;
        }
        public IActionResult Index()
        {
            IList<Park> parkNames = surveyDAO.GetParkNames();
            ViewData["parkNames"] = parkNames;

            return View();
        }
        public IActionResult SurveyResults()
        {
            var survey = surveyDAO.GetSurveys();
            return View(survey);
        }
        [HttpPost]
        public IActionResult SaveNewSurvey(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                IList<Park> parkNames = surveyDAO.GetParkNames();
                ViewData["parkNames"] = parkNames;
                return View("Index",survey);
            }
            else
            {
                surveyDAO.SaveNewSurvey(survey);

                return RedirectToAction("SurveyResults");
            }
            
        }
    }
}