using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class SurveyResultsModel
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public int AnnualVisitorCount { get; set; }
        public string ParkDescription { get; set; }
        public int SurveyCount { get; set; }
    }
}
