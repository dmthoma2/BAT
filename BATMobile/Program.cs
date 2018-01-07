using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAT_Models.Algorithm;
using BAT_Services;
using Ninject;
using System.Reflection;
using BAT_Models;
using BAT_Models.API;

namespace BATMobile
{
    /// <summary>
    /// BATMobile is a automated Binance API cryptocurrency trading application.
    /// It is configuration driven and suppored by a SOA for trading logic and trade execution.
    /// </summary>
    public class Program
    {
        private Guid RunID;
        private IParametersService _iParametersService;
        private IAlgorithmService _iAlgorithmService;
        private ITradeService _iTradeService;
        private ILogService _iLogService;
        private IEmailService _iEmailService;

        public Program(IParametersService iParametersService, IAlgorithmService iAlgorithmService, ITradeService iTradeService, ILogService iLogService, IEmailService iEmailService)
        {
            _iParametersService = iParametersService;
            _iAlgorithmService = iAlgorithmService;
            _iTradeService = iTradeService;
            _iLogService = iLogService;
            _iEmailService = iEmailService;
            RunID = Guid.NewGuid();
        }

        static void Main(string[] args)
        {
            StandardKernel _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());
            
            Program prog = new Program(_kernel.Get<IParametersService>(), 
                _kernel.Get<IAlgorithmService>(), 
                _kernel.Get<ITradeService>(),
                _kernel.Get<ILogService>(),
                _kernel.Get<IEmailService>());

            //Parse configuration into a model
            var parameters = prog.LoadParameters();
                   
            //Call trading algorithm
            var trades = prog.ExecuteAlgorithm(parameters);
       
            //Execute trades based on results
            prog.ExecuteTrades(trades);

        }//Main

        /// <summary>
        /// Loads app settings into memory and emails the user information about the application start.
        /// </summary>
        public Parameters LoadParameters()
        {
            var output = _iParametersService.GetConfigurationSettings();

            //Send loading email if indicated.
            //TODO
            
            return output;
        }//LoadParameters

        /// <summary>
        /// Executes the indicated algorithm and returns a list of trades to execute based on the results.  Also sends an email with findings if indicated.
        /// </summary>
        public List<Trade> ExecuteAlgorithm(Parameters parameters)
        {
            //Find Prices

            //Log Price Finding

            var output = _iAlgorithmService.ExecuteREBALANCE(new REBALANCE_Params(parameters));

            //Log Algorithm Results

            //Send algorithm email
            //TODO
               

            return output;
        }//ExecuteAlgorithm

        public void ExecuteTrades(List<Trade> trades)
        {
            foreach (var trade in trades)
            {
                var result = _iTradeService.ExecuteTrade(trade);
            }//foreach
            
            //Log Results

            //Send results email
            //TODO

        }//ExecuteTrades

    }//Program
}
