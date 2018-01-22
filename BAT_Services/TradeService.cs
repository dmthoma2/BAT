using BAT_Models.API;
using System;
using System.Collections.Generic;
using System.Text;
using BAT_Repository;

namespace BAT_Services
{
    public interface ITradeService
    {
        void ExecuteTrade(Trade trade, string BaseCurrency, string APIKey, string SecretKey, bool TestTrade);
    }//ITradeService

    /// <summary>
    /// This service contains methods associated with trade executions.
    /// </summary>
    public class TradeService : ITradeService
    {
        private static IAPIRepository _apiRepository;

        public TradeService(IAPIRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }//Constructor

        /// <summary>
        /// Executes a trade through the Binance API.
        /// </summary>
        /// <param name="trade">The trade to execute.</param>
        /// <returns>A summary of the executed trade.</returns>
        public void ExecuteTrade(Trade trade, string BaseCurrency, string APIKey, string SecretKey, bool TestTrade)
        {
            _apiRepository.ExecuteTrade(trade.Symbol + BaseCurrency, trade.TradeType, trade.Amount, APIKey, SecretKey, TestTrade);
            
        }//ExecuteTrade

    }//TradeService
}
