using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.Account;
using Binance.Account.Orders;
using Binance.Api;
using BAT_Models.API;
namespace BAT_Repository
{
    public interface IAPIRepository
    {
        decimal GetPrice(string Symbol);
        List<AccountBalance> GetAccountBalances(string APIKey, string SecretKey);
        void ExecuteTrade(string Symbol, string Type, decimal Amount, string APIKey, string SecretKey, bool TestTrade = true);
    }//IAPIRepsoitory

    public class APIRepository : IAPIRepository
    {
        private BinanceApi _binanceApi;

        public APIRepository()
        {
            var client = new BinanceHttpClient();

            _binanceApi = new BinanceApi(client, null);

        }//APIRepository

        public decimal GetPrice(string Symbol)
        {
            var price = _binanceApi.GetPriceAsync(Symbol);

            return price.Result.Value;
        }//GetPrice

        public List<AccountBalance> GetAccountBalances(string APIKey, string SecretKey)
        {
            var accountInfo = _binanceApi.GetAccountInfoAsync(new BinanceApiUser(APIKey, SecretKey));
            
            return accountInfo.Result.Balances.OrderBy(x => x.Asset).ToList();
        }//GetAccountBalances

        public void ExecuteTrade(string Symbol, string Type, decimal Amount, string APIKey, string SecretKey, bool TestTrade = true)
        {
            var user = new BinanceApiUser(APIKey, SecretKey);

            MarketOrder marketOrder = new MarketOrder(user);
            if (Type == Trade.TradeTypes.BUY)
            {
                marketOrder.Side = OrderSide.Buy;
            }//if
            else
            {
                marketOrder.Side = OrderSide.Sell;
            }//else
            marketOrder.Symbol = Symbol;
            marketOrder.Quantity = Amount;

            if (TestTrade)
            {
                _binanceApi.TestPlaceAsync(marketOrder);

            }//if
            else
            {
                _binanceApi.PlaceAsync(marketOrder);
            }//else
        }//ExecuteTrade

    }//API Repository
}
