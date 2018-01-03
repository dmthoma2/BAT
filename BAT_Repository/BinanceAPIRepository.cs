using System;
using System.Collections.Generic;
using System.Text;

namespace BAT_Repository
{
    public interface IBinanceAPIRepository
    {

    }//IBinanceAPIRepository

    /// <summary>
    /// Contains methods for sending and receiving Binance API calls.
    /// </summary>
    public class BinanceAPIRepository : IBinanceAPIRepository
    {
        private string APIAddress;

        /// <param name="APIAddress">Required nonnull field.</param>
        public BinanceAPIRepository(string APIAddress)
        {
            this.APIAddress = APIAddress;
        }//BinanceAPIRepository



    }//BinanceAPIRepository
}
