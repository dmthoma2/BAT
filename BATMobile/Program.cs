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
            try
            {
                Console.WriteLine("Launching BAT (Binance Auto Trader)!");

                StandardKernel _kernel = new StandardKernel();
                _kernel.Load(Assembly.GetExecutingAssembly());

                Program prog = new Program(_kernel.Get<IParametersService>(),
                    _kernel.Get<IAlgorithmService>(),
                    _kernel.Get<ITradeService>(),
                    _kernel.Get<ILogService>(),
                    _kernel.Get<IEmailService>());

                Console.WriteLine("Loading Parameters! ");
                //Parse configuration into a model
                var parameters = prog.LoadParameters();

                Console.WriteLine("Beginning Algorithm!");
                //Call trading algorithm
                var trades = prog.ExecuteAlgorithm(parameters);

                Console.WriteLine("Beginning Trading!");
                //Execute trades based on results
                prog.ExecuteTrades(trades);

            }//try
            catch(Exception e)
            {
                Console.WriteLine("Exception occurred!  Message: " + e.Message);
            }//catch
           

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();

        }//Main

        /// <summary>
        /// Loads app settings into memory and emails the user information about the application start.
        /// </summary>
        public Parameters LoadParameters()
        {
            var output = _iParametersService.GetConfigurationSettings();

            //Send loading email if indicated.
            if (output.SendLoadingEmail)
            {
                var emailBody = _iEmailService.GetLoadingEmailBody(output);

                Console.WriteLine("Parameter Loading Results!");
                Console.WriteLine(emailBody);

                var subject = "BAT Loading - " + output.Algo;

                if (emailBody.Contains("Aborting run!"))
                { subject += " - ERROR"; }//if

                _iEmailService.SendEmail(output.InformationEmailAddress, output.BATsEmailAddress,
                        output.BATsEmailPW, output.SMTPServer, subject, emailBody);

                if (emailBody.Contains("Aborting run!"))
                {
                    throw new Exception("Unable to connect to markets and verify currencies.  Unable to run algorithm or trade.  Closing Application!");
                }//if
            }//if
            
            return output;
        }//LoadParameters

        /// <summary>
        /// Executes the indicated algorithm and returns a list of trades to execute based on the results.  Also sends an email with findings if indicated.
        /// </summary>
        public List<Trade> ExecuteAlgorithm(Parameters parameters)
        {
            //Find Prices

            //NLog Price Finding

            var output = _iAlgorithmService.ExecuteREBALANCE(new REBALANCE_Params(parameters));

            //NLog Algorithm Results

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
            
            //Log File Results

            //Send results email
            //TODO

        }//ExecuteTrades

    }//Program
}
