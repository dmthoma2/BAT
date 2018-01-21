using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAT_Services;
using BAT_Repository;
using BAT_Utilities;
using Moq;
using Binance.Account;
using System.Collections.Generic;

namespace BAT_Tests
{
    [TestClass]
    public class AlgorithmServiceTest
    {

        AlgorithmService _algorithmService;
        ParametersService _parametersService;
        Mock<IAppSettings> _appSettings;
        Mock<IAPIRepository> _apiRepo;
        Mock<IInformationService> _infoService;
        private List<AccountBalance> AccountBalances;

        [TestInitialize]
        public void Initialize()
        {
            _apiRepo = new Mock<IAPIRepository>();
            _infoService = new Mock<IInformationService>();
            _appSettings = new Mock<IAppSettings>();
            _algorithmService = new AlgorithmService(_apiRepo.Object, _infoService.Object);
            _parametersService = new ParametersService(_appSettings.Object);

            SetDefaultAppSettings();
        }//Initialize


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
            _appSettings.Setup(x => x.Currency4MinimumTradeUnit()).Returns(1);
            _appSettings.Setup(x => x.Currency4InitialAllocation()).Returns(12);


            AccountBalances = new List<AccountBalance>();
            AccountBalances.Add(new AccountBalance("BTC", 1.000m, 0.0m));
            AccountBalances.Add(new AccountBalance("XRP", 3.333m, 0.0m));
            AccountBalances.Add(new AccountBalance("IOTA", 5.00m, 0.0m));
            AccountBalances.Add(new AccountBalance("ETH", .001m, 0.0m));
            AccountBalances.Add(new AccountBalance("ADA", 77.000m, 0.0m));

            _infoService.Setup(x => x.GetPrice("XRPBTC")).Returns(.00001m);
            _infoService.Setup(x => x.GetPrice("IOTABTC")).Returns(.003m);
            _infoService.Setup(x => x.GetPrice("ETHBTC")).Returns(.175m);
            _infoService.Setup(x => x.GetPrice("ADABTC")).Returns(.0000001m);
        }

        [TestMethod]
        public void GetAlgoParamsTest()
        {
            SetDefaultAppSettings();
            var appParams = _parametersService.GetConfigurationSettings();

            var algoParams = _algorithmService.GetAlgoParams(appParams, AccountBalances);

            Assert.IsNotNull(algoParams);
            Assert.AreEqual("BTC", algoParams.BaseCurrency);
            Assert.AreEqual(1.000m, algoParams.BaseCurrencyBalance);
            Assert.AreEqual(10, algoParams.BaseCurrencyTargetAllocation);
            Assert.AreEqual(5, algoParams.RebalanceThreshold);

            Assert.AreEqual("XRP", algoParams.Currencies[0].Symbol);
            Assert.AreEqual(.00001m, algoParams.Currencies[0].PriceInBaseCurrency);
            Assert.AreEqual(10, algoParams.Currencies[0].TargetAllocation);
            Assert.AreEqual(1m, algoParams.Currencies[0].MinimumTradingUnit);
            Assert.AreEqual(3.333m, algoParams.Currencies[0].Quanity);

            Assert.AreEqual("IOTA", algoParams.Currencies[1].Symbol);
            Assert.AreEqual(.003m, algoParams.Currencies[1].PriceInBaseCurrency);
            Assert.AreEqual(20, algoParams.Currencies[1].TargetAllocation);
            Assert.AreEqual(1m, algoParams.Currencies[1].MinimumTradingUnit);
            Assert.AreEqual(5.000m, algoParams.Currencies[1].Quanity);

            Assert.AreEqual("ETH", algoParams.Currencies[2].Symbol);
            Assert.AreEqual(.175m, algoParams.Currencies[2].PriceInBaseCurrency);
            Assert.AreEqual(25, algoParams.Currencies[2].TargetAllocation);
            Assert.AreEqual(.001m, algoParams.Currencies[2].MinimumTradingUnit);
            Assert.AreEqual(.001m, algoParams.Currencies[2].Quanity);

            Assert.AreEqual("ADA", algoParams.Currencies[3].Symbol);
            Assert.AreEqual(.0000001m, algoParams.Currencies[3].PriceInBaseCurrency);
            Assert.AreEqual(35, algoParams.Currencies[3].TargetAllocation);
            Assert.AreEqual(1m, algoParams.Currencies[3].MinimumTradingUnit);
            Assert.AreEqual(77m, algoParams.Currencies[3].Quanity);

            SetDefaultAppSettings();
            appParams = _parametersService.GetConfigurationSettings();
            appParams.Currency2 = null;
            appParams.Currency3 = string.Empty;
            appParams.Currency4 = "      ";

            algoParams = _algorithmService.GetAlgoParams(appParams, AccountBalances);

            Assert.IsNotNull(algoParams);
            Assert.AreEqual(1, algoParams.Currencies.Count);
        }//GetAlgoParamsTest
    }
}
