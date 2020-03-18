using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public int FiveDayForecastValue { get; set; }
        public double LowTemp { get; set; }
        public double HighTemp { get; set; }
        public string Forecast { get; set; }
        public string WeatherAdvice { get; set; }
        public bool Farenheit { get; set; }
        public double DisplayLowTemp
        {
            get
            {
                
                if (Farenheit)
                {
                    return Math.Round(LowTemp);
                }
                else
                {
                    return Math.Round((LowTemp - 32) * (5.0 / 9));
                }
                
            }
        }
       
        public double DisplayHighTemp
        {
            get
            {
                if (Farenheit)
                {
                    return Math.Round(HighTemp);
                }
                else
                {
                    return Math.Round((HighTemp - 32) * (5.0/9));
                }
            }
        }
        
        public Dictionary<string, string> WeatherDict = new Dictionary<string, string>()
            {
                {"rain", "Pack raingear and wear waterproof shoes." },
                {"snow", "Pack snowshoes." },
                {"thunderstorms","Seek shelter. Avoid hiking on exposed ridges." },
                {"clear-day","Pack Sunblock" },
                {"partly-cloudy-day","Is it partly cloudy, or partly sunny? " },
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
