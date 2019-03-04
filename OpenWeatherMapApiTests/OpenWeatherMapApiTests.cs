using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenWeatherMapApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherMapApi.Tests
{
    [TestClass()]
    public class OpenWeatherMapApi
    {
        string Token = "0d49f0f17ba0dcaa80d206b0d74d5a5e"; //token from OpenWeatherMap
        [TestMethod()]
        public void SetParams()
        {
            OpenWeatherMap OWM = new OpenWeatherMap(Token);
            OWM.Lang = "us";
            Assert.AreEqual("us", OWM.Lang);
            OWM.unit = OpenWeatherMap.Units.metric;
            Assert.AreEqual("&units=metric", OWM.SetUnits(OWM.unit));
         
        }

        [TestMethod()]
        public void CurrentTest()
        {
            OpenWeatherMap OWM = new OpenWeatherMap(Token);
            Assert.AreEqual("Moscow", OWM.Current("moscow").name);
        }

        [TestMethod()]
        public void ForecastTest()
        {
            OpenWeatherMap OWM = new OpenWeatherMap(Token);
            Assert.AreEqual("Moscow", OWM.Forecast("moscow").city.name);
        }
    }
}