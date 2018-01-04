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

        public void SetDefaultAppSettings()
        {
            _appSettings.Setup(x => x.BinanceAPIAddress()).Returns("https://api.binance.com/");
            _appSettings.Setup(x => x.APIKey()).Returns("abc123");
            _appSettings.Setup(x => x.RebalanceThreshold()).Returns(5);
            _appSettings.Setup(x => x.BaseCurrency()).Returns("BTC");
            _appSettings.Setup(x => x.BaseCurrencyAllocation()).Returns(10);
            _appSettings.Setup(x => x.BaseCurrencyInitialAllocation()).Returns(0);
        }

        [TestMethod]
        public void GetAPIInformationTest()
        {
            SetDefaultAppSettings();

            var output = _parametersService.GetAPIInformation(new Parameters());

            Assert.AreEqual("https://api.binance.com/", output.BinanceAPIAddress);
            Assert.AreEqual("abc123", output.APIKey);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.BinanceAPIAddress()).Returns(string.Empty);
            
            try
            { output = _parametersService.GetAPIInformation(new Parameters()); Assert.Fail(); }
            catch(ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No BinanceAPIAddress supplied.  A URL must be provided to connect.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.BinanceAPIAddress()).Returns((string)null);

            try
            { output = _parametersService.GetAPIInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No BinanceAPIAddress supplied.  A URL must be provided to connect.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.APIKey()).Returns(string.Empty);

            try
            { output = _parametersService.GetAPIInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No APIKey supplied.  A key must be provided to pass security crednetials with Binance.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.APIKey()).Returns((string)null);

            try
            { output = _parametersService.GetAPIInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No APIKey supplied.  A key must be provided to pass security crednetials with Binance.")); }//catch

        }//GetAPIInformationTest

        [TestMethod]
        public void GetBaseCurrencyInformationTest()
        {
            SetDefaultAppSettings();
            var output = _parametersService.GetBaseCurrencyInformation(new Parameters());

            Assert.AreEqual(5, output.RebalanceThreshold);
            Assert.AreEqual("BTC", output.BaseCurrency);
            Assert.AreEqual(10, output.BaseCurrencyAllocation);
            Assert.AreEqual(0, output.BaseCurrencyInitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.BaseCurrencyAllocation()).Returns(90);
            _appSettings.Setup(x => x.BaseCurrencyInitialAllocation()).Returns(-10);
            output = _parametersService.GetBaseCurrencyInformation(new Parameters());

            Assert.AreEqual(5, output.RebalanceThreshold);
            Assert.AreEqual("BTC", output.BaseCurrency);
            Assert.AreEqual(90, output.BaseCurrencyAllocation);
            Assert.AreEqual(0, output.BaseCurrencyInitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.RebalanceThreshold()).Returns(4);
            
            try
            { output = _parametersService.GetBaseCurrencyInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Reblance threashold has a minimum value of 5.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.RebalanceThreshold()).Returns(0);

            try
            { output = _parametersService.GetBaseCurrencyInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Reblance threashold has a minimum value of 5.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.BaseCurrency()).Returns((string)null);
           
            try
            { output = _parametersService.GetBaseCurrencyInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No base currency supplied.  This is a required field for rebalancing.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.BaseCurrency()).Returns(string.Empty);

            try
            { output = _parametersService.GetBaseCurrencyInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No base currency supplied.  This is a required field for rebalancing.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.BaseCurrencyAllocation()).Returns(9);
            try
            { output = _parametersService.GetBaseCurrencyInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Base Currency must have an allocation between 10 and 90.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.BaseCurrencyAllocation()).Returns(91);
            try
            { output = _parametersService.GetBaseCurrencyInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Base Currency must have an allocation between 10 and 90.")); }//catch
            
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

        [TestMethod]
        public void VerifyREBALANCETotalsTest()
        {
            //TODO
        }//VerifyREBALANCETotalsTest
    }
}
