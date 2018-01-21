using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAT_Services;
using BAT_Repository;
using BAT_Models.Exceptions;
using Binance.Account;
using System.Collections.Generic;
using Moq;

namespace BAT_Tests
{
    [TestClass]
    public class InformationServiceTest
    {

        InformationService _informationService;
        Mock<IAPIRepository> _apiRepo;

        [TestInitialize]
        public void Initialize()
        {
            _apiRepo = new Mock<IAPIRepository>();
            _informationService = new InformationService(_apiRepo.Object);
        }//Initialize

        [TestMethod]
        public void GetPriceTest()
        {
            _apiRepo.Setup(x => x.GetPrice("BTC")).Returns(1.00m);

            var output = _informationService.GetPrice("BTC");

            Assert.AreEqual(1.00m, output);
            
            try
            { output = _informationService.GetPrice(string.Empty); Assert.Fail(); }
            catch (InformationException ie)
            { Assert.IsTrue(ie.Message.Contains("Cannot retrieve pricing information with a null/empty symbol.")); }//catch

        }//GetPriceTest

        [TestMethod]
        public void GetAccountHoldingsTest()
        {
            var balanceResults = new List<AccountBalance>();

            balanceResults.Add(new AccountBalance("BTC", 1.000m, 0.0m));
            balanceResults.Add(new AccountBalance("IOTA", 3.333m, 0.0m));
            balanceResults.Add(new AccountBalance("ETH", 5.00m, 0.0m));

            _apiRepo
                .Setup(x => x.GetAccountBalances(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(balanceResults);
                      
            var output = _informationService.GetAccountHoldings("ABC123", "SECRETPASSCODE");

            Assert.IsNotNull(output);
            Assert.AreEqual(3, output.Count);
            Assert.AreEqual("BTC", output[0].Asset);
            Assert.AreEqual(1.00m, output[0].Free);
            Assert.AreEqual("IOTA", output[1].Asset);
            Assert.AreEqual(3.333m, output[1].Free);
            Assert.AreEqual("ETH", output[2].Asset);
            Assert.AreEqual(5.00m, output[2].Free);

            try
            { output = _informationService.GetAccountHoldings(null, "SECRETPASSCODE"); Assert.Fail(); }
            catch (InformationException ie)
            { Assert.IsTrue(ie.Message.Contains("Cannot retrieve account balance information with a null/empty API key.")); }//catch
            
            try
            { output = _informationService.GetAccountHoldings( "ABC123", null); Assert.Fail(); }
            catch (InformationException ie)
            { Assert.IsTrue(ie.Message.Contains("Cannot retrieve account balance information with a null/empty secret key.")); }//catch

            _apiRepo
                .Setup(x => x.GetAccountBalances(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((List<AccountBalance>)null);

            try
            { output = _informationService.GetAccountHoldings("ABC123", "SECRETPASSCODE"); Assert.Fail(); }
            catch (InformationException ie)
            { Assert.IsTrue(ie.Message.Contains("Account has no balance or it unable to retrieved.")); }//catch

        }//GetPriceTest
    }
}
