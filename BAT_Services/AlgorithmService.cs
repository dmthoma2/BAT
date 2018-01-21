using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAT_Models.API;
using BAT_Models.Algorithm;
using BAT_Models;
using Binance.Account;
using BAT_Repository;
namespace BAT_Services
{
    public interface IAlgorithmService
    {
        List<Trade> ExecuteREBALANCE(REBALANCE_Params parameters);
        REBALANCE_Params GetAlgoParams(Parameters parameters, List<AccountBalance> balances);
    }//IAlgorithmService

    /// <summary>
    /// This service contains various trading algorithm implemntations.
    /// </summary>
    public class AlgorithmService : IAlgorithmService
    {
        private static IInformationService _informationService;
        private static IAPIRepository _apiRepository;

        public AlgorithmService(IAPIRepository apiRepository, IInformationService informationService)
        {
            _apiRepository = apiRepository;
            _informationService = informationService;
        }//Constructor

        public REBALANCE_Params GetAlgoParams(Parameters parameters, List<AccountBalance> balances)
        {
            var output = new REBALANCE_Params();

            output.BaseCurrency = parameters.BaseCurrency;
            output.BaseCurrencyBalance = balances.FirstOrDefault(x => x.Asset == parameters.BaseCurrency).Free;
            output.BaseCurrencyTargetAllocation = parameters.BaseCurrencyAllocation;
            output.RebalanceThreshold = parameters.RebalanceThreshold;

            output.Currencies = new List<Asset>();
            var currency1 = new Asset();
            currency1.Symbol = parameters.Currency1;
            currency1.Quanity = balances.FirstOrDefault(x => x.Asset == parameters.Currency1).Free;
            currency1.TargetAllocation = parameters.Currency1Allocation;
            currency1.MinimumTradingUnit = parameters.Currency1MinimumTradeUnit;
            currency1.PriceInBaseCurrency = _informationService.GetPrice(parameters.Currency1 + parameters.BaseCurrency);
            output.Currencies.Add(currency1);

            var currency2 = new Asset();
            currency2.Symbol = parameters.Currency2;
            if (!string.IsNullOrWhiteSpace(parameters.Currency2))
            {
                currency2.Quanity = balances.FirstOrDefault(x => x.Asset == parameters.Currency2).Free;
                currency2.TargetAllocation = parameters.Currency2Allocation;
                currency2.MinimumTradingUnit = parameters.Currency2MinimumTradeUnit;
                currency2.PriceInBaseCurrency = _informationService.GetPrice(parameters.Currency2 + parameters.BaseCurrency);
                output.Currencies.Add(currency2);
            }//if
            
            var currency3 = new Asset();
            currency3.Symbol = parameters.Currency3;
            if (!string.IsNullOrWhiteSpace(parameters.Currency3))
            {
                currency3.Quanity = balances.FirstOrDefault(x => x.Asset == parameters.Currency3).Free;
                currency3.TargetAllocation = parameters.Currency3Allocation;
                currency3.MinimumTradingUnit = parameters.Currency3MinimumTradeUnit;
                currency3.PriceInBaseCurrency = _informationService.GetPrice(parameters.Currency3 + parameters.BaseCurrency);
                output.Currencies.Add(currency3);
            }//if
            
            var currency4 = new Asset();
            currency4.Symbol = parameters.Currency4;            
            if (!string.IsNullOrWhiteSpace(parameters.Currency4))
            {
                currency4.Quanity = balances.FirstOrDefault(x => x.Asset == parameters.Currency4).Free;
                currency4.TargetAllocation = parameters.Currency4Allocation;
                currency4.MinimumTradingUnit = parameters.Currency4MinimumTradeUnit;
                currency4.PriceInBaseCurrency = _informationService.GetPrice(parameters.Currency4 + parameters.BaseCurrency);
                output.Currencies.Add(currency4);
            }//if
            
            return output;
        }//GetAlgoParams


        public List<Trade> ExecuteREBALANCE(REBALANCE_Params parameters)
        {
            decimal AccountValue = parameters.BaseCurrencyBalance + parameters.Currencies.Sum(x => x.Quanity * x.PriceInBaseCurrency);

            List<Trade> output = new List<Trade>();
            
            foreach (var crypto in parameters.Currencies)
            {
                //Calculate rebalance thresholds for this currency
                decimal targetBalance = parameters.BaseCurrencyBalance * ((decimal)crypto.TargetAllocation / parameters.BaseCurrencyTargetAllocation);
                decimal rebalanceDelta = targetBalance * (parameters.RebalanceThreshold / 100m);
                decimal lowerBound = targetBalance - rebalanceDelta;
                decimal upperBound = targetBalance + rebalanceDelta;

                decimal value = crypto.PriceInBaseCurrency * crypto.Quanity;

                if(value < lowerBound)
                {
                    //Calculate units to buy as the number of trading units needs to be bought to bring to bring this crypto into balance.  Only need to execute 1/2 the number of trades because funds raised from the trades will adjust the target.
                    var unitsToBuy = Math.Floor((Math.Abs((parameters.BaseCurrencyBalance - value)) / crypto.PriceInBaseCurrency) / crypto.MinimumTradingUnit / 2);
                    output.Add(new Trade()
                    {
                        Symbol = crypto.Symbol,
                        TradeType = Trade.TradeTypes.BUY,
                        Amount = unitsToBuy

                    });
                }//if
                else if (value > upperBound)
                {
                    //Calculate units to sell as the number of trading units needs to be bought to bring to bring this crypto into balance.  Only need to execute 1/2 the number of trades because funds raised from the trades will adjust the target.
                    var unitsToSell = Math.Floor((Math.Abs((parameters.BaseCurrencyBalance - value)) / crypto.PriceInBaseCurrency) / crypto.MinimumTradingUnit / 2) ;
                    output.Add(new Trade()
                    {
                        Symbol = crypto.Symbol,
                        TradeType = Trade.TradeTypes.SELL,
                        Amount = unitsToSell
                    });
                }//if


            }//foreach



            return new List<Trade>();
        }//ExecuteREBALANCE

    }//AlgorithmService
}
