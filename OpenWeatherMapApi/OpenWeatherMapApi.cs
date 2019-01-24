using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenWeatherMapApi
{
    public class OpenWeatherMapApi
    {
        readonly string openWeatherLink = "https://api.openweathermap.org/data/2.5/";
        readonly List<string> specialParams = new List<string>();
        readonly string token;

        private string lang = "&lang=us";
        /// <summary>
        /// set lang (Arabic - ar, Bulgarian - bg, Catalan - ca, Czech - cz, German - de, Greek - el, English - en, Persian (Farsi) - fa, Finnish - fi, French - fr, Galician - gl, Croatian - hr, Hungarian - hu, Italian - it, Japanese - ja, Korean - kr, Latvian - la, Lithuanian - lt, Macedonian - mk, Dutch - nl, Polish - pl, Portuguese - pt, Romanian - ro, Russian - ru, Swedish - se, Slovak - sk, Slovenian - sl, Spanish - es, Turkish - tr, Ukrainian - ua, Vietnamese - vi, Chinese Simplified - zh_cn, Chinese Traditional - zh_tw.)
        /// </summary>
        public string Lang { get { return lang.Split('=')[1]; } set { lang = "&lang=" + value; } }



        private string units = "&units=metric";
        /// <summary>
        /// Units of request, can be Fahrenheit(use "imperial") or Celsius(use "metric") or Kelvin(use "kelvin")
        /// </summary>
        public string Units
        {
            get { if (units == null) return "kelvin"; else return units.Split('=')[1]; }
            set
            {
                if (value == "imperial" || value == "metric") units = "&units=" + value;
                else if (value == "kelvin") units = null;
            }
        }

        public WebProxy proxy; //прокси

        private bool useProxy { get; set; }
        /// <summary>
        /// set true proxy only if proxy is no null
        /// </summary>
        public bool UseProxy { get { return useProxy; } set { if (proxy == null) useProxy = false; else if (value == true) useProxy = true; } }


        /// <summary>
        /// Init
        /// </summary>
        /// <param name="_token">ключ</param>
        public OpenWeatherMapApi(string _token)
        {
            token = _token;
        }


        /// <summary>
        /// Init with special params
        /// </summary>
        /// <param name="_token">your token</param>
        /// <param name="_units">temp units (can be Fahrenheit(use "imperial") or Celsius(use "metric") or Kelvin(use "kelvin")) </param>
        /// <param name="_lang">set lang (Arabic - ar, Bulgarian - bg, Catalan - ca, Czech - cz, German - de, Greek - el, English - en, Persian (Farsi) - fa, Finnish - fi, French - fr, Galician - gl, Croatian - hr, Hungarian - hu, Italian - it, Japanese - ja, Korean - kr, Latvian - la, Lithuanian - lt, Macedonian - mk, Dutch - nl, Polish - pl, Portuguese - pt, Romanian - ro, Russian - ru, Swedish - se, Slovak - sk, Slovenian - sl, Spanish - es, Turkish - tr, Ukrainian - ua, Vietnamese - vi, Chinese Simplified - zh_cn, Chinese Traditional - zh_tw.)</param>
        public OpenWeatherMapApi(string _token, string _units, string _lang)
        {
            token = _token;
            Units = _units;
            Lang = _lang;
        }



        /// <summary>
        /// Get current 
        /// </summary>
        ///  <param name="_location">city name (example "moscow"), or by geographic coordinates(example "35.3,50.2")</param>
        /// <returns></returns>
        public Current Current(string _location)
        {
            string response = Download("weather?" , _location );
            Current processedResponse = JsonConvert.DeserializeObject<Current>(response);
            return processedResponse;
        }
        /// <summary>
        /// Get current 
        /// </summary>
        ///  <param name="_location">city name (example "moscow"), or by geographic coordinates(example "35.3,50.2")</param>
        /// <returns></returns>
        public Forecast Forecast(string _location)
        {
            string response = Download("forecast?", _location);
            Forecast processedResponse = JsonConvert.DeserializeObject<Forecast>(response);
            return processedResponse;
        }


        /// <summary>
        /// Downloading func
        /// </summary>
        /// <param name="_dataRequestType"> accordance with conditions openWeatherMap </param>
        /// <param name="_location">location</param>
        /// <returns></returns>
        private string Download(string _dataRequestType, string _location)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(
                openWeatherLink 
                + _dataRequestType 
                + processedLocation(_location) 
                + lang + units
                + "&appid=" + token); //concat request link
            if (useProxy) httpWebRequest.Proxy = proxy; //если стоит флаг использования прокси то используем
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse(); //делаем запрос
            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            return response;
        }


        /// <summary>
        /// preparation "location name" accordance with conditions openWeatherMap
        /// </summary>
        /// <param name="_location">city name (example "moscow"), or by geographic coordinates(example "35.3,50.2")</param>
        /// <returns></returns>
        private string processedLocation(string _location)
        {
            //если есть запятая, значит работаем с координатами
            if (_location.IndexOf(",") != -1)
            {
                return "lat=" + _location.Split(',')[0] + "&lon=" + _location.Split(',')[1];
            }
            //если нет запятой, значит это имя города. работаем как с городами.
            else
            {
                return "q=" + _location;
            }
        }



    }

    #region responces 

    public class Forecast
    {
        public string cod { get; set; }
        public float message { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
        public City city { get; set; }
    }
    public class Current
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
        public float pressure { get; set; }
        public float sea_level { get; set; }
        public float grnd_level { get; set; }
        public float humidity { get; set; }
        public float temp_kf { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }
        public float deg { get; set; }
    }

    public class Snow
    {
        public float __invalid_name__3h { get; set; }
    }

    public class Sys
    {
        public string pod { get; set; }
    }

    public class List
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public Snow snow { get; set; }
        public Sys sys { get; set; }
        public string dt_txt { get; set; }
    }

    public class Coord
    {
        public float lat { get; set; }
        public float lon { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
        public int population { get; set; }
    }

   
    #endregion



}
