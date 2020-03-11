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
            
            return View();
        }
        public IActionResult SurveyResults()
        {
            var survey = surveyDAO.GetSurveys();
            return View(survey);
        }
        public IActionResult SaveNewSurvey(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            surveyDAO.SaveNewSurvey(survey);

            return RedirectToAction("SurveyResults");
        }
    }
}