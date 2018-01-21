using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.Account;
using Binance.Api;
namespace BAT_Repository
{
    public interface IAPIRepository
    {
        decimal GetPrice(string Symbol);
        List<AccountBalance> GetAccountBalances(string APIKey, string SecretKey);
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
        }//GetPrice

    }//API Repository
}
