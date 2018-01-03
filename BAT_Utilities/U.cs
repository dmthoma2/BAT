using System;
using System.Collections.Generic;
using System.Text;

namespace BAT_Utilities
{
    /// <summary>
    /// Utilities class for convient access to frequent boilerplate code.
    /// </summary>
    public static class U
    {


        /// <summary>
        /// Convient Helper methods to retrieve "AppSettings" values from a configuration file.
        /// </summary>
        public static class AppSettings
        {
            public static int RebalanceThreshold()
            { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["RebalanceThreshold"]); }//RebalanceThreshold

            public static string BaseCurrency()
            { return System.Configuration.ConfigurationManager.AppSettings["BaseCurrency"]; }//BaseCurrency

            public static int BaseCurrencyAllocation()
            { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["BaseCurrencyAllocation"]); }//BaseCurrencyAllocation

            public static decimal BaseCurrencyInitialAllocation()
            { return decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["BaseCurrencyInitialAllocation"]); }//BaseCurrencyInitialAllocation

            public static string Currency1()
            { return System.Configuration.ConfigurationManager.AppSettings["Currency1"]; }//Currency1

            public static int Currency1Allocation()
            { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency1Allocation"]); }//Currency1Allocation

            public static decimal Currency1InitialAllocation()
            { return decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency1InitialAllocation"]); }//Currency1InitialAllocation

            public static string Currency2()
            { return System.Configuration.ConfigurationManager.AppSettings["Currency2"]; }//Currency2

            public static int Currency2Allocation()
            { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency2Allocation"]); }//Currency2Allocation

            public static decimal Currency2InitialAllocation()
            { return decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency2InitialAllocation"]); }//Currency2InitialAllocation

            public static string Currency3()
            { return System.Configuration.ConfigurationManager.AppSettings["Currency3"]; }//Currency3

            public static int Currency3Allocation()
            { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency3Allocation"]); }//Currency3Allocation

            public static decimal Currency3InitialAllocation()
            { return decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency3InitialAllocation"]); }//Currency3InitialAllocation

            public static string Currency4()
            { return System.Configuration.ConfigurationManager.AppSettings["Currency4"]; }//Currency4

            public static int Currency4Allocation()
            { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency4Allocation"]); }//Currency4Allocation

            public static decimal Currency4InitialAllocation()
            { return decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["Currency4InitialAllocation"]); }//Currency4InitialAllocation

            public static bool UseCircuitBreaker()
            { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["UseCircuitBreaker"]); }//UseCircuitBreaker

            public static int CircuitBreakerTrades()
            { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["CircuitBreakerTrades"]); }//CircuitBreakerTrades

            public static int CircuitBreakerHours()
            { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["CircuitBreakerHours"]); }//CircuitBreakerHours

            public static string BATsEmailAddress()
            { return System.Configuration.ConfigurationManager.AppSettings["BATsEmailAddress"]; }//BATsEmailAddress

            public static string SMTPServer()
            { return System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]; }//SMTPServer

            public static string InformationEmailAddress()
            { return System.Configuration.ConfigurationManager.AppSettings["InformationEmailAddress"]; }//InformationEmailAddress

            public static bool BuyAndHoldComparison()
            { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["BuyAndHoldComparison"]); }//BuyAndHoldComparison

            public static bool SendLoadingEmail()
            { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SendLoadingEmail"]); }//SendLoadingEmail

            public static bool SendAlgorithmEmail()
            { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SendAlgorithmEmail"]); }//SendAlgorithmEmail

            public static bool SendTradeExecutionEmail()
            { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SendTradeExecutionEmail"]); }//SendTradeExecutionEmail

            public static bool FailOnError()
            { return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["FailOnError"]); }//FailOnError
            
        }//AppSettings

    }//U
}
