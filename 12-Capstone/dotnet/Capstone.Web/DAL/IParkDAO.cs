using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IParkDAO
    {
        Park GetPark(string parkCode);
        IList<Park> GetAllParks();
        Park GetParkDetails(string parkCode);

    }
}
