using System;
using System.Collections.Generic;
using System.Text;
using BAT_Models.Exceptions;
using BAT_Repository;
using Binance.Account;
using Binance.Api;


namespace BAT_Services
{
    public interface IInformationService
    {
        decimal GetPrice(string Symbol);
        List<AccountBalance> GetAccountHoldings(string APIKey, string SecretKey);
    }//IInformationService

    /// <summary>
    /// This service returns current market values and account balance information.
    /// </summary>
    public class InformationService : IInformationService
    {
        private IAPIRepository _apiRepository;

        public InformationService(IAPIRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }//InformationService

        public decimal GetPrice(string Symbol)
        {
            if (string.IsNullOrWhiteSpace(Symbol))
            {
                throw new InformationException("Cannot retrieve pricing information with a null/empty symbol.");
            }//if

            var price = _apiRepository.GetPrice(Symbol);

            return price;
        }//GetPrice

        public List<AccountBalance> GetAccountHoldings(string APIKey, string SecretKey)
        {
            if (string.IsNullOrWhiteSpace(APIKey))
            {
                throw new InformationException("Cannot retrieve account balance information with a null/empty API key.");
            }//if

            if (string.IsNullOrWhiteSpace(SecretKey))
            {
                throw new InformationException("Cannot retrieve account balance information with a null/empty secret key.");
            }//if

            var price = _apiRepository.GetAccountBalances(APIKey, SecretKey);

            if(price == null || price.Count == 0)
            {
                throw new InformationException("Account has no balance or it unable to retrieved.");
            }//if

            return price;
        }//GetAccountHoldings

    }//InformationService
}
