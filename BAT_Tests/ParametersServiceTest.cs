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
            _appSettings.Setup(x => x.APITradingKey()).Returns("def456");           
            _appSettings.Setup(x => x.UseCircuitBreaker()).Returns(true);
            _appSettings.Setup(x => x.CircuitBreakerTrades()).Returns(3);
            _appSettings.Setup(x => x.CircuitBreakerHours()).Returns(24);
            _appSettings.Setup(x => x.BATsEmailAddress()).Returns("test@noreply.com");
            _appSettings.Setup(x => x.BATsEmailPW()).Returns("abc123");
            _appSettings.Setup(x => x.SMTPServer()).Returns("temp.smtp.com");
            _appSettings.Setup(x => x.InformationEmailAddress()).Returns("testEmail@gmail.com");
            _appSettings.Setup(x => x.SendLoadingEmail()).Returns(true);
            _appSettings.Setup(x => x.SendAlgorithmEmail()).Returns(true);
            _appSettings.Setup(x => x.SendTradeExecutionEmail()).Returns(true);
            _appSettings.Setup(x => x.BuyAndHoldComparison()).Returns(true);
            _appSettings.Setup(x => x.FailOnError()).Returns(true);
            _appSettings.Setup(x => x.HistoryFile()).Returns("whatever.txt");
            _appSettings.Setup(x => x.Algo()).Returns("REBALANCE");

            _appSettings.Setup(x => x.RebalanceThreshold()).Returns(5);
            _appSettings.Setup(x => x.BaseCurrency()).Returns("BTC");
            _appSettings.Setup(x => x.BaseCurrencyAllocation()).Returns(10);
            _appSettings.Setup(x => x.BaseCurrencyInitialAllocation()).Returns(0);
            _appSettings.Setup(x => x.Currency1()).Returns("XRP");
            _appSettings.Setup(x => x.Currency1Allocation()).Returns(10);
            _appSettings.Setup(x => x.Currency1MinimumTradeUnit()).Returns(1);
            _appSettings.Setup(x => x.Currency1InitialAllocation()).Returns(33);
            _appSettings.Setup(x => x.Currency2()).Returns("IOTA");
            _appSettings.Setup(x => x.Currency2Allocation()).Returns(20);
            _appSettings.Setup(x => x.Currency2MinimumTradeUnit()).Returns(1);
            _appSettings.Setup(x => x.Currency2InitialAllocation()).Returns(6);
            _appSettings.Setup(x => x.Currency3()).Returns("ETH");
            _appSettings.Setup(x => x.Currency3Allocation()).Returns(25);
            _appSettings.Setup(x => x.Currency3MinimumTradeUnit()).Returns(.001m);
            _appSettings.Setup(x => x.Currency3InitialAllocation()).Returns((decimal).5);
            _appSettings.Setup(x => x.Currency4()).Returns("ADA");
            _appSettings.Setup(x => x.Currency4Allocation()).Returns(35);
            _appSettings.Setup(x => x.Currency4MinimumTradeUnit()).Returns(0);
            _appSettings.Setup(x => x.Currency4InitialAllocation()).Returns(12);
        }

        [TestMethod]
        public void GetAPIInformationTest()
        {
            SetDefaultAppSettings();

            var output = _parametersService.GetAPIInformation(new Parameters());

            Assert.AreEqual("https://api.binance.com/", output.BinanceAPIAddress);
            Assert.AreEqual("abc123", output.APIKey);
            Assert.AreEqual("def456", output.APITradingKey);

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
            { Assert.IsTrue(ce.Message.Contains("No APIKey supplied.  A key must be provided to pass security credentials with Binance.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.APIKey()).Returns((string)null);

            try
            { output = _parametersService.GetAPIInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No APIKey supplied.  A key must be provided to pass security credentials with Binance.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.APITradingKey()).Returns(string.Empty);

            try
            { output = _parametersService.GetAPIInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No APITradingKey supplied.  A key must be provided to authorize trading and account access with Binance.")); }//catch


            SetDefaultAppSettings();
            _appSettings.Setup(x => x.APITradingKey()).Returns((string)null);

            try
            { output = _parametersService.GetAPIInformation(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No APITradingKey supplied.  A key must be provided to authorize trading and account access with Binance.")); }//catch

        }//GetAPIInformationTest
                
        [TestMethod]
        public void GetCircuitBreakerInformationTest()
        {
            SetDefaultAppSettings();
            var output = _parametersService.GetCircuitBreakerInformation(new Parameters());

            Assert.AreEqual(true, output.UseCircuitBreaker);
            Assert.AreEqual(3, output.CircuitBreakerTrades);
            Assert.AreEqual(24, output.CircuitBreakerHours);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.UseCircuitBreaker()).Returns(false);
            _appSettings.Setup(x => x.CircuitBreakerTrades()).Returns(0);
            _appSettings.Setup(x => x.CircuitBreakerHours()).Returns(0);

            output = _parametersService.GetCircuitBreakerInformation(new Parameters());
            
            Assert.AreEqual(false, output.UseCircuitBreaker);
            Assert.AreEqual(1, output.CircuitBreakerTrades);
            Assert.AreEqual(1, output.CircuitBreakerHours);

        }//GetCircuitBreakerInformationTest

        [TestMethod]
        public void GetNotificationSettingsTest()
        {
            SetDefaultAppSettings();
            var output = _parametersService.GetNotificationSettings(new Parameters());

            Assert.AreEqual("test@noreply.com", output.BATsEmailAddress);
            Assert.AreEqual("temp.smtp.com", output.SMTPServer);
            Assert.AreEqual("testEmail@gmail.com", output.InformationEmailAddress);
            Assert.AreEqual(true, output.SendLoadingEmail);
            Assert.AreEqual(true, output.SendAlgorithmEmail);
            Assert.AreEqual(true, output.SendTradeExecutionEmail);
            Assert.AreEqual(true, output.BuyAndHoldComparison);
            Assert.AreEqual(true, output.BuyAndHoldComparison);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.BATsEmailAddress()).Returns((string)null);
            _appSettings.Setup(x => x.BATsEmailPW()).Returns((string)null);
            _appSettings.Setup(x => x.SMTPServer()).Returns((string)null);
            _appSettings.Setup(x => x.SendLoadingEmail()).Returns(false);
            _appSettings.Setup(x => x.SendAlgorithmEmail()).Returns(false);
            _appSettings.Setup(x => x.SendTradeExecutionEmail()).Returns(false);
            _appSettings.Setup(x => x.BuyAndHoldComparison()).Returns(false);
            _appSettings.Setup(x => x.FailOnError()).Returns(false);
            _appSettings.Setup(x => x.InformationEmailAddress()).Returns((string)null);
            output = _parametersService.GetNotificationSettings(new Parameters());

            Assert.AreEqual("BinanceAutoTrader@gmail.com", output.BATsEmailAddress);
            Assert.AreEqual("ABC123DEF", output.BATsEmailPW);
            Assert.AreEqual("smtp.gmail.com", output.SMTPServer);
            Assert.AreEqual(string.Empty, output.InformationEmailAddress);
            Assert.AreEqual(false, output.SendLoadingEmail);
            Assert.AreEqual(false, output.SendAlgorithmEmail);
            Assert.AreEqual(false, output.SendTradeExecutionEmail);
            Assert.AreEqual(false, output.BuyAndHoldComparison);
            Assert.AreEqual(false, output.BuyAndHoldComparison);
            
            SetDefaultAppSettings();
            _appSettings.Setup(x => x.InformationEmailAddress()).Returns((string)null);

            try
            { output = _parametersService.GetNotificationSettings(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("An email address is required to send emails.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.HistoryFile()).Returns((string)null);

            try
            { output = _parametersService.GetNotificationSettings(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("A history file location is required.")); }//catch
            
        }//GetNotificationSettingsTest

        [TestMethod]
        public void GetAlgorithmTypeTest()
        {
            SetDefaultAppSettings();

            var output = _parametersService.GetAlgorithmType(new Parameters());

            Assert.AreEqual("REBALANCE", output.Algo);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Algo()).Returns((string)null);

            try
            { output = _parametersService.GetAlgorithmType(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Unrecognized trading algorithm.")); }//catch
            
        }//GetAlgorithmTypeTest

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
        public void GetCurrency1InformationTest()
        {
            SetDefaultAppSettings();
            var output = _parametersService.GetCurrency1Information(new Parameters());

            Assert.AreEqual("XRP", output.Currency1);
            Assert.AreEqual(10, output.Currency1Allocation);
            Assert.AreEqual(33, output.Currency1InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency1Allocation()).Returns(90);
            _appSettings.Setup(x => x.Currency1MinimumTradeUnit()).Returns(0);
            _appSettings.Setup(x => x.Currency1InitialAllocation()).Returns(-10);
            output = _parametersService.GetCurrency1Information(new Parameters());

            Assert.AreEqual("XRP", output.Currency1);
            Assert.AreEqual(90, output.Currency1Allocation);
            Assert.AreEqual(1, output.Currency1MinimumTradeUnit);
            Assert.AreEqual(0, output.Currency1InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency1()).Returns((string)null);

            try
            { output = _parametersService.GetCurrency1Information(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No Currency1 supplied.  This is a required field for rebalancing.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency1()).Returns(string.Empty);

            try
            { output = _parametersService.GetCurrency1Information(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("No Currency1 supplied.  This is a required field for rebalancing.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency1Allocation()).Returns(9);

            try
            { output = _parametersService.GetCurrency1Information(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Currency1 must have an allocation between 10 and 90.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency1Allocation()).Returns(91);

            try
            { output = _parametersService.GetCurrency1Information(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Currency1 must have an allocation between 10 and 90.")); }//catch



        }//GetCurrency1Information

        [TestMethod]
        public void GetCurrency2InformationTest()
        {
            SetDefaultAppSettings();
            var output = _parametersService.GetCurrency2Information(new Parameters());

            Assert.AreEqual("IOTA", output.Currency2);
            Assert.AreEqual(20, output.Currency2Allocation);
            Assert.AreEqual(1, output.Currency2MinimumTradeUnit);
            Assert.AreEqual(6, output.Currency2InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency2Allocation()).Returns(90);
            _appSettings.Setup(x => x.Currency2MinimumTradeUnit()).Returns(0);
            _appSettings.Setup(x => x.Currency2InitialAllocation()).Returns(-10);
            output = _parametersService.GetCurrency2Information(new Parameters());

            Assert.AreEqual("IOTA", output.Currency2);
            Assert.AreEqual(90, output.Currency2Allocation);
            Assert.AreEqual(1, output.Currency2MinimumTradeUnit);
            Assert.AreEqual(0, output.Currency2InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency2()).Returns((string)null);

            output = _parametersService.GetCurrency2Information(new Parameters());

            Assert.AreEqual(string.Empty, output.Currency2);
            Assert.AreEqual(0, output.Currency2Allocation);            
            Assert.AreEqual(0, output.Currency2InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency2()).Returns(string.Empty);

            output = _parametersService.GetCurrency2Information(new Parameters());

            Assert.AreEqual(string.Empty, output.Currency2);
            Assert.AreEqual(0, output.Currency2Allocation);
            Assert.AreEqual(0, output.Currency2InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency2Allocation()).Returns(9);

            try
            { output = _parametersService.GetCurrency2Information(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Currency2 must have an allocation between 10 and 90.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency2Allocation()).Returns(91);

            try
            { output = _parametersService.GetCurrency2Information(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Currency2 must have an allocation between 10 and 90.")); }//catch

        }//GetCurrency2Information

        [TestMethod]
        public void GetCurrency3InformationTest()
        {
            SetDefaultAppSettings();
            var output = _parametersService.GetCurrency3Information(new Parameters());

            Assert.AreEqual("ETH", output.Currency3);
            Assert.AreEqual(25, output.Currency3Allocation);
            Assert.AreEqual(.001m, output.Currency3MinimumTradeUnit);
            Assert.AreEqual(.5m, output.Currency3InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency3Allocation()).Returns(90);
            _appSettings.Setup(x => x.Currency3MinimumTradeUnit()).Returns(0);
            _appSettings.Setup(x => x.Currency3InitialAllocation()).Returns(-10);
            output = _parametersService.GetCurrency3Information(new Parameters());

            Assert.AreEqual("ETH", output.Currency3);
            Assert.AreEqual(90, output.Currency3Allocation);
            Assert.AreEqual(1, output.Currency3MinimumTradeUnit);
            Assert.AreEqual(0, output.Currency3InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency3()).Returns((string)null);

            output = _parametersService.GetCurrency3Information(new Parameters());

            Assert.AreEqual(string.Empty, output.Currency3);
            Assert.AreEqual(0, output.Currency3Allocation);
            Assert.AreEqual(0, output.Currency3InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency3()).Returns(string.Empty);

            output = _parametersService.GetCurrency3Information(new Parameters());

            Assert.AreEqual(string.Empty, output.Currency3);
            Assert.AreEqual(0, output.Currency3Allocation);
            Assert.AreEqual(0, output.Currency3InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency3Allocation()).Returns(9);

            try
            { output = _parametersService.GetCurrency3Information(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Currency3 must have an allocation between 10 and 90.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency3Allocation()).Returns(91);

            try
            { output = _parametersService.GetCurrency3Information(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Currency3 must have an allocation between 10 and 90.")); }//catch

        }//GetCurrency3Information

        [TestMethod]
        public void GetCurrency4InformationTest()
        {
            SetDefaultAppSettings();
            var output = _parametersService.GetCurrency4Information(new Parameters());

            Assert.AreEqual("ADA", output.Currency4);
            Assert.AreEqual(35, output.Currency4Allocation);
            Assert.AreEqual(1, output.Currency4MinimumTradeUnit);
            Assert.AreEqual(12, output.Currency4InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency4Allocation()).Returns(90);
            _appSettings.Setup(x => x.Currency4MinimumTradeUnit()).Returns(0);
            _appSettings.Setup(x => x.Currency4InitialAllocation()).Returns(-10);
            output = _parametersService.GetCurrency4Information(new Parameters());

            Assert.AreEqual("ADA", output.Currency4);
            Assert.AreEqual(90, output.Currency4Allocation);
            Assert.AreEqual(1, output.Currency4MinimumTradeUnit);
            Assert.AreEqual(0, output.Currency4InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency4()).Returns((string)null);

            output = _parametersService.GetCurrency4Information(new Parameters());

            Assert.AreEqual(string.Empty, output.Currency4);
            Assert.AreEqual(0, output.Currency4Allocation);
            Assert.AreEqual(0, output.Currency4InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency4()).Returns(string.Empty);

            output = _parametersService.GetCurrency4Information(new Parameters());

            Assert.AreEqual(string.Empty, output.Currency4);
            Assert.AreEqual(0, output.Currency4Allocation);
            Assert.AreEqual(0, output.Currency4InitialAllocation);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency4Allocation()).Returns(9);

            try
            { output = _parametersService.GetCurrency4Information(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Currency4 must have an allocation between 10 and 90.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency4Allocation()).Returns(91);

            try
            { output = _parametersService.GetCurrency4Information(new Parameters()); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Currency4 must have an allocation between 10 and 90.")); }//catch

        }//GetCurrency4Information


        [TestMethod]
        public void VerifyREBALANCETotalsTest()
        {
            SetDefaultAppSettings();
            var output = _parametersService.GetBaseCurrencyInformation(new Parameters());
            output = _parametersService.GetCurrency1Information(output);
            output = _parametersService.GetCurrency2Information(output);
            output = _parametersService.GetCurrency3Information(output);
            output = _parametersService.GetCurrency4Information(output);
            _parametersService.VerifyREBALANCETotals(output);

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.BaseCurrencyAllocation()).Returns(50);
            output = _parametersService.GetBaseCurrencyInformation(new Parameters());
            output = _parametersService.GetCurrency1Information(output);
            output = _parametersService.GetCurrency2Information(output);
            output = _parametersService.GetCurrency3Information(output);
            output = _parametersService.GetCurrency4Information(output);

            try
            { _parametersService.VerifyREBALANCETotals(output); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Total allocation of all currencies must equal 100!.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency1Allocation()).Returns(50);
            output = _parametersService.GetBaseCurrencyInformation(new Parameters());
            output = _parametersService.GetCurrency1Information(output);
            output = _parametersService.GetCurrency2Information(output);
            output = _parametersService.GetCurrency3Information(output);
            output = _parametersService.GetCurrency4Information(output);

            try
            { _parametersService.VerifyREBALANCETotals(output); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Total allocation of all currencies must equal 100!.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency2Allocation()).Returns(50);
            output = _parametersService.GetBaseCurrencyInformation(new Parameters());
            output = _parametersService.GetCurrency1Information(output);
            output = _parametersService.GetCurrency2Information(output);
            output = _parametersService.GetCurrency3Information(output);
            output = _parametersService.GetCurrency4Information(output);

            try
            { _parametersService.VerifyREBALANCETotals(output); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Total allocation of all currencies must equal 100!.")); }//catch
            
            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency3Allocation()).Returns(50);
            output = _parametersService.GetBaseCurrencyInformation(new Parameters());
            output = _parametersService.GetCurrency1Information(output);
            output = _parametersService.GetCurrency2Information(output);
            output = _parametersService.GetCurrency3Information(output);
            output = _parametersService.GetCurrency4Information(output);

            try
            { _parametersService.VerifyREBALANCETotals(output); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Total allocation of all currencies must equal 100!.")); }//catch

            SetDefaultAppSettings();
            _appSettings.Setup(x => x.Currency4Allocation()).Returns(50);
            output = _parametersService.GetBaseCurrencyInformation(new Parameters());
            output = _parametersService.GetCurrency1Information(output);
            output = _parametersService.GetCurrency2Information(output);
            output = _parametersService.GetCurrency3Information(output);
            output = _parametersService.GetCurrency4Information(output);

            try
            { _parametersService.VerifyREBALANCETotals(output); Assert.Fail(); }
            catch (ConfigurationException ce)
            { Assert.IsTrue(ce.Message.Contains("Total allocation of all currencies must equal 100!.")); }//catch
            
        }//VerifyREBALANCETotalsTest
    }
}
