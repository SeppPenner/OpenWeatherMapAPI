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
    public class OpenWeatherMapApiTests
    {
        [TestMethod()]
        public void SetParams()
        {
            OpenWeatherMapApi OWM = new OpenWeatherMapApi("token");
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
            Assert.Fail();
        }

        [TestMethod()]
        public void ForecastTest()
        {
            Assert.Fail();
        }
    }
}