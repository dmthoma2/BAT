using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAT_Models.API
{
    public class Trade
    {
        public string Symbol { get; set; }
        public string TradeType { get; set; }
        public decimal Amount { get; set; }


        public static class TradeTypes
        {
            public const string BUY = "BUY";
            public const string SELL = "SELL";
        }//TradeTypes
    }//Trade
}
