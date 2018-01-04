using BAT_Models.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAT_Services
{
    public interface ITradeService
    {
        TradeResult ExecuteTrade(Trade trade);
    }//ITradeService

    /// <summary>
    /// This service contains methods associated with trade executions.
    /// </summary>
    public class TradeService : ITradeService
    {
        /// <summary>
        /// Executes a trade through the Binance API.
        /// </summary>
        /// <param name="trade">The trade to execute.</param>
        /// <returns>A summary of the executed trade.</returns>
        public TradeResult ExecuteTrade(Trade trade)
        {
            //TODO

            return new TradeResult();
        }//ExecuteTrade

    }//TradeService
}
