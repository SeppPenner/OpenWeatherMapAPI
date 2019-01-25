using OpenWeatherMapApi;
using System;
using System.Linq;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenWeatherMap weather = new OpenWeatherMap("apiKey", "metric", "us");

            // (!) I use a proxy, because my server is in the corporate networkwork
            //weather.proxy = new System.Net.WebProxy("proxy:3128");
            //weather.UseProxy = true;

            //Current
            var moscowCurrent =  weather.Current("Moscow");
            Console.WriteLine(@"{0} : {1} {2}C°", moscowCurrent.name ,moscowCurrent.weather.First().description , moscowCurrent.main.temp);

            //Forecast
            string cityName = "London";
            foreach (var list in weather.Forecast(cityName).list)
            {
                //new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(list.dt) = convert unixtime to datetime
                Console.WriteLine( @"{0} {1} : {2}C°, {3}", cityName, new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(list.dt), list.main.temp , list.weather.First().description);
            }


            Console.ReadKey();
        }
    }
}
