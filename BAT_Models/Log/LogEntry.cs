using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAT_Models.Log
{
    public class LogEntry
    {
        public const char DELIMITER = '|';
        public const string ERROR_CODE = "ERROR";


        public Guid TransactionGroup { get; set; }
        public DateTime TransactionTime { get; set; }
        public string Symbol { get; set; }
        public string TradeType { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ErrorMessage { get; set; }

        public bool HasError()
        { return Symbol != null && Symbol.ToUpper().Trim() == ERROR_CODE; }//HasError

    }//LogEntry
}
