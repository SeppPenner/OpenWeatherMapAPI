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
        string Token = "SECRET"; //token from OpenWeatherMap
        [TestMethod()]
        public void SetParams()
        {
            OpenWeatherMap OWM = new OpenWeatherMap(Token);
            OWM.Lang = "us";
            Assert.AreEqual("us", OWM.Lang);
            OWM.Units = "metric";
            Assert.AreEqual("metric", OWM.Units);
            OWM.Units = "failUnits";
            Assert.AreNotEqual("failUnits", OWM.Units);
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