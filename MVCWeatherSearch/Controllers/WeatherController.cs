using MVCWeatherSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVCWeatherSearch.Controllers
{
    public class WeatherController : Controller
    {
        // GET: Weather
        public ActionResult Index()
        {
            return View(new WeatherViewModel());
        }

        [HttpPost]
        public ActionResult Index(string RequestedCity)
        {
            string appId = "1e447f1c7fee1e044947f13c1107e220";
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", RequestedCity, appId);

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

                WeatherViewModel rslt = new WeatherViewModel();
                rslt.RequestedCity = RequestedCity;
                rslt.Country = weatherInfo.sys.country;
                rslt.City = weatherInfo.name;
                rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                rslt.Description = weatherInfo.weather[0].description;
                rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                rslt.WeatherIcon = weatherInfo.weather[0].icon;
                return View(rslt);
            }
        }
    }
}