using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAT_Services;
using System.Reflection;
using Ninject;
using BAT_Models;
using BAT_Models.Exceptions;
using BAT_Utilities;
using Moq;

namespace BAT_Tests
{
    [TestClass]
    public class ParametersServiceTest
    {
        ParametersService _parametersService;
        Mock<IAppSettings> _appSettings;

        [TestInitialize]
        public void Initialize()
        {
           _appSettings = new Mock<IAppSettings>();
           _parametersService = new ParametersService(_appSettings.Object);
        }

        [TestMethod]
        public void GetAPIInformationTest()
        {
            _appSettings.Setup(x => x.BinanceAPIAddress()).Returns("https://api.binance.com/");
            _appSettings.Setup(x => x.APIKey()).Returns("abc123");

            var output = _parametersService.GetAPIInformation(new Parameters());

            Assert.AreEqual("https://api.binance.com/", output.BinanceAPIAddress);
            Assert.AreEqual("abc123", output.APIKey);

            _appSettings.Setup(x => x.BinanceAPIAddress()).Returns(string.Empty);
            _appSettings.Setup(x => x.APIKey()).Returns("abc123");

            try
            { output = _parametersService.GetAPIInformation(new Parameters()); Assert.Fail(); }
            catch(ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No BinanceAPIAddress supplied.  A URL must be provided to connect.")); }//catch

            _appSettings.Setup(x => x.BinanceAPIAddress()).Returns((string)null);
            _appSettings.Setup(x => x.APIKey()).Returns("abc123");

            try
            { output = _parametersService.GetAPIInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No BinanceAPIAddress supplied.  A URL must be provided to connect.")); }//catch

            _appSettings.Setup(x => x.BinanceAPIAddress()).Returns(("https://api.binance.com/"));
            _appSettings.Setup(x => x.APIKey()).Returns(string.Empty);

            try
            { output = _parametersService.GetAPIInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No APIKey supplied.  A key must be provided to pass security crednetials with Binance.")); }//catch

            _appSettings.Setup(x => x.BinanceAPIAddress()).Returns(("https://api.binance.com/"));
            _appSettings.Setup(x => x.APIKey()).Returns((string)null);

            try
            { output = _parametersService.GetAPIInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No APIKey supplied.  A key must be provided to pass security crednetials with Binance.")); }//catch

        }//GetAPIInformationTest

        [TestMethod]
        public void GetBaseCurrencyInformationTest()
        {
            //TODO
        }//GetBaseCurrencyInformationTest

        [TestMethod]
        public void GetCurrencyInformationTest()
        {
            //TODO
        }//GetCurrencyInformation

        [TestMethod]
        public void GetCircuitBreakerInformationTest()
        {
            //TODO
        }//GetCircuitBreakerInformationTest

        [TestMethod]
        public void GetNotificationSettingsTest()
        {
            //TODO
        }//GetNotificationSettingsTest
    }
}
