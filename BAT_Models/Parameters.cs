using System;
using System.Collections.Generic;
using System.Text;

namespace BAT_Models
{
    /// <summary>
    /// A model to represent configuration parameters for use within the application.
    /// </summary>
    public class Parameters
    {
        public bool SuccessfullyPopulated { get; set; }
        public string ErrorMessage = string.Empty;

        public string BinanceAPIAddress { get; set; }
        public string APIKey { get; set; }

        public int RebalanceThreshold { get; set; }

        public string BaseCurrency { get; set; }
        public int BaseCurrencyAllocation { get; set; }
        public decimal BaseCurrencyInitialAllocation { get; set; }

        public string Currency1 { get; set; }
        public int Currency1Allocation { get; set; }
        public decimal Currency1InitialAllocation { get; set; }

        public string Currency2 { get; set; }
        public int Currency2Allocation { get; set; }
        public decimal Currency2InitialAllocation { get; set; }

        public string Currency3 { get; set; }
        public int Currency3Allocation { get; set; }
        public decimal Currency3InitialAllocation { get; set; }

        public string Currency4 { get; set; }
        public int Currency4Allocation { get; set; }
        public decimal Currency4InitialAllocation { get; set; }

        public bool UseCircuitBreaker { get; set; }
        public int CircuitBreakerTrades { get; set; }
        public int CircuitBreakerHours { get; set; }

        public string BATsEmailAddress { get; set; }
        public string SMTPServer { get; set; }
        public string InformationEmailAddress { get; set; }
        public bool BuyAndHoldComparison { get; set; }
        public bool SendLoadingEmail { get; set; }
        public bool SendAlgorithmEmail { get; set; }
        public bool SendTradeExecutionEmail { get; set; }
        public bool FailOnError { get; set; }
    }//Parameters
}
