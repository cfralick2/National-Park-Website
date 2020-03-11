using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public int FiveDayForecastValue { get; set; }
        public int LowTemp { get; set; }
        public int HighTemp { get; set; }
        public string Forecast { get; set; }
        public string WeatherAdvice { get; set; }
        public bool Farenheit { get; set; }

        //public string GetWeatherAdvice()
        //{

        //string weatherAdvice = "";
        //if (Forecast == "rain")
        //{
        //    weatherAdvice = "Pack raingear and wear waterproof shoes.";
        //}
        //if (Forecast == "snow")
        //{
        //    weatherAdvice = "Pack snowshoes.";
        //}
        //if (Forecast == "thunderstorms")
        //{
        //    weatherAdvice = "Seek shelter. Avoid hiking on exposed ridges.";
        //}
        //if (Forecast == "sun")
        //{
        //    weatherAdvice = "Pack sunblock.";
        //}
        //return weatherAdvice;


        public Dictionary<string, string> WeatherDict = new Dictionary<string, string>()
            {
                {"rain", "Pack raingear and wear waterproof shoes." },
                {"snow", "Pack snowshoes." },
                {"thunderstorms","Seek shelter. Avoid hiking on exposed ridges." },
                {"sunny","Pack Sunblock" },
                {"partly cloudy","Is it partly cloudy, or partly sunny? " },
                {"cloudy", "Watch out for Vampires, for they walk the land today. " }
            };
        public string GetTempAdvice()
        {
            string tempAdvice = "";
      
            if (HighTemp > 75)
            {
                tempAdvice = "Bring an extra gallon of water.";
            }
            else if (LowTemp < 20)
            {
                tempAdvice = "WARNING! Exposure to frigid temperatures can be very dangerous.";
            }
            else
            {
                tempAdvice = "Mild. ";
            }
            if (HighTemp - LowTemp > 20)
            {
                tempAdvice += " Wear breathable layers.";
            }

            return tempAdvice;
        }
    }
}
