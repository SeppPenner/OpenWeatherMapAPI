# OpenWeatherMapAPI for free servises
My release of openweathermap api for net framework 4.6.1
-----

# Download dll
http://eliseevtech.ru/files/openweathermap/OpenWeatherMapApi.dll
# Usage
First - get your api key from openweathermap.org
</br>Next add OpenWeatherMapApi.dll to your project

## Create api class:
</br>OpenWeatherMap(YOU_API_KEY);
</br>   or 
</br>    OpenWeatherMap(YOU_API_KEY, UNITS, LANG) 
>        UNITS can be: OpenWeatherMap.Units.imperial, OpenWeatherMap.Units.metric, OpenWeatherMap.Units.kelvin
>        LANG can be: "ru", "us" , "pl", "zh_cn" and other, you can find it on openweathermap.org
example(there is not work random key, change it on your key!): 

    OpenWeatherMap weather = new OpenWeatherMap("d312d32131d314fcd4214d124d14"); 
    //or 
    OpenWeatherMap weather = new OpenWeatherMap("d312d32131d314fcd4214d124d14", OpenWeatherMap.Units.metric, "ru"); 

   
 
## Get current weather

    var moscowCurrent =  weather.Current("Moscow"); //cache data
    Console.WriteLine(@"{0} : {1} {2} C°", moscowCurrent.name ,moscowCurrent.weather.First().description , moscowCurrent.main.temp);

## Get forecast weather

    string cityName = "London";
        foreach (var list in weather.Forecast(cityName).list)
        {
            // (*) new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(list.dt) it is convert unixtime to datetime
            Console.WriteLine( @"{0} {1} : {2}C°, {3}", cityName, new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(list.dt), list.main.temp , list.weather.First().description);
        }


