using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAT_Models.Algorithm
{
    /// <summary>
    /// A container representing a single crypto.
    /// </summary>
    public class Asset
    {
        public string Symbol { get; set; }
        public decimal Quanity { get; set; }
        public int TargetAllocation { get; set; }
        public decimal MinimumTradingUnit { get; set; }
        public decimal PriceInBaseCurrency { get; set; }
    }//Asset
}
