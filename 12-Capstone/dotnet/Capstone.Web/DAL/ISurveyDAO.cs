﻿using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface ISurveyDAO
    {
        bool SaveNewSurvey(Survey survey);
        IList<SurveyResultsModel> GetSurveys();
        IList<Park> GetParkNames();
    }
}
