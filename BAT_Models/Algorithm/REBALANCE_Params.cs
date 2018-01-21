using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.Account;

namespace BAT_Models.Algorithm
{
    public class REBALANCE_Params
    {        
        //Target Allocation
        public string BaseCurrency { get; set; }
        public decimal BaseCurrencyBalance { get; set; }
        public int BaseCurrencyTargetAllocation { get; set; }
        public int RebalanceThreshold { get; set; }
        //Exisiting Balances
        public List<Asset> Currencies { get; set; }
        
        

    }//REBALANCE_Params
}
