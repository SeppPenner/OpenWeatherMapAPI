using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherMapApi
{
    class Program
    {
        public static WebProxy webProxy;
        static void Main(string[] args)
        {
            ///// example and tests
            OpenWeatherMapApi Api = new OpenWeatherMapApi("secret api key", "metric", "ru");
            if (webProxy == null) Console.WriteLine("Dasdasd");
            Api.UseProxy = true;
            Console.WriteLine(Api.Current("moscow").main.temp);

            Console.WriteLine(Api.Forecast("moscow").list.First().main.temp);
           
            
            Console.ReadKey();
        }
    }
}
