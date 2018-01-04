using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAT_Utilities
{
    public interface IAppSettings
    {
        string BinanceAPIAddress();
        string APIKey();
        int RebalanceThreshold();
        string BaseCurrency();
        int BaseCurrencyAllocation();
        decimal BaseCurrencyInitialAllocation();
        string Currency1();
        int Currency1Allocation();
        decimal Currency1InitialAllocation();
        string Currency2();
        int Currency2Allocation();
        decimal Currency2InitialAllocation();
        string Currency3();
        int Currency3Allocation();
        decimal Currency3InitialAllocation();
        string Currency4();
        int Currency4Allocation();
        decimal Currency4InitialAllocation();
        bool UseCircuitBreaker();
        int CircuitBreakerTrades();
        int CircuitBreakerHours();
        string BATsEmailAddress();
        string SMTPServer();
        string InformationEmailAddress();
        bool BuyAndHoldComparison();
        bool SendLoadingEmail();
        bool SendAlgorithmEmail();
        bool SendTradeExecutionEmail();
        bool FailOnError();
    }//IAppSettings

    /// <summary>
    /// Convient Helper methods to retrieve "AppSettings" values from a configuration file.
    /// </summary>
    public class AppSettings : IAppSettings
    {
        public string BinanceAPIAddress()
        { return System.Configuration.ConfigurationManager.AppSettings["BinanceAPIAddress"]; }//BinanceAPIAddress

        public string APIKey()
        { return System.Configuration.ConfigurationManager.AppSettings["APIKey"]; }//APIKey

        public int RebalanceThreshold()
        { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["RebalanceThreshold"]); }//RebalanceThreshold

        public string BaseCurrency()
        { return System.Configuration.ConfigurationManager.AppSettings["BaseCurrency"]; }//BaseCurrency

        public int BaseCurrencyAllocation()
        { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["BaseCurrencyAllocation"]); }//BaseCurrencyAllocation

        public decimal BaseCurrencyInitialAllocation()
        { return decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["BaseCurrencyInitialAllocation"]); }//BaseCurrencyInitialAllocation

        public string Currency1()
        { return System.Configuration.ConfigurationManager.AppSettings["Currency1"]; }//Currency1

        public int Currency1Allocation()
        { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency1Allocation"]); }//Currency1Allocation

        public decimal Currency1InitialAllocation()
        { return decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency1InitialAllocation"]); }//Currency1InitialAllocation

        public string Currency2()
        { return System.Configuration.ConfigurationManager.AppSettings["Currency2"]; }//Currency2

        public int Currency2Allocation()
        { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency2Allocation"]); }//Currency2Allocation

        public decimal Currency2InitialAllocation()
        { return decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency2InitialAllocation"]); }//Currency2InitialAllocation

        public string Currency3()
        { return System.Configuration.ConfigurationManager.AppSettings["Currency3"]; }//Currency3

        public int Currency3Allocation()
        { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency3Allocation"]); }//Currency3Allocation

        public decimal Currency3InitialAllocation()
        { return decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency3InitialAllocation"]); }//Currency3InitialAllocation

        public string Currency4()
        { return System.Configuration.ConfigurationManager.AppSettings["Currency4"]; }//Currency4

        public int Currency4Allocation()
        { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency4Allocation"]); }//Currency4Allocation

        public decimal Currency4InitialAllocation()
        { return decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency4InitialAllocation"]); }//Currency4InitialAllocation

        public bool UseCircuitBreaker()
        { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["UseCircuitBreaker"]); }//UseCircuitBreaker

        public int CircuitBreakerTrades()
        { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["CircuitBreakerTrades"]); }//CircuitBreakerTrades

        public int CircuitBreakerHours()
        { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["CircuitBreakerHours"]); }//CircuitBreakerHours

        public string BATsEmailAddress()
        { return System.Configuration.ConfigurationManager.AppSettings["BATsEmailAddress"]; }//BATsEmailAddress

        public string SMTPServer()
        { return System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]; }//SMTPServer

        public string InformationEmailAddress()
        { return System.Configuration.ConfigurationManager.AppSettings["InformationEmailAddress"]; }//InformationEmailAddress

        public bool BuyAndHoldComparison()
        { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["BuyAndHoldComparison"]); }//BuyAndHoldComparison

        public bool SendLoadingEmail()
        { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SendLoadingEmail"]); }//SendLoadingEmail

        public bool SendAlgorithmEmail()
        { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SendAlgorithmEmail"]); }//SendAlgorithmEmail

        public bool SendTradeExecutionEmail()
        { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SendTradeExecutionEmail"]); }//SendTradeExecutionEmail

        public bool FailOnError()
        { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["FailOnError"]); }//FailOnError

    }//AppSettings
}
