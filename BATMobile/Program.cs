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
        private IInformationService _iInformationService;
        private IAlgorithmService _iAlgorithmService;
        private ITradeService _iTradeService;
        private ILogService _iLogService;
        private IEmailService _iEmailService;

        public Program(IParametersService iParametersService, IInformationService iInformationService, IAlgorithmService iAlgorithmService, ITradeService iTradeService, ILogService iLogService, IEmailService iEmailService)
        {
            _iParametersService = iParametersService;
            _iInformationService = iInformationService;
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
                    _kernel.Get<IInformationService>(),
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

                if(trades != null && trades.Any())
                {
                    Console.WriteLine("Beginning Trading!");
                    //Execute trades based on results
                    prog.ExecuteTrades(trades, parameters);
                }//if              

            }//try
            catch(Exception e)
            {
                Console.WriteLine("Exception occurred!  Message: " + e.Message);
            }//catch
           

            //Console.WriteLine("Press any key to continue...");
            //Console.ReadLine();

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

                subject += " - ID " + RunID.ToString();

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
            var balances = _iInformationService.GetAccountHoldings(parameters.APIKey, parameters.APITradingKey);
            var balanceParameters = _iAlgorithmService.GetAlgoParams(parameters, balances);

            //NLog Price Finding
            //TODO
            

            var trades = _iAlgorithmService.ExecuteREBALANCE(balanceParameters);

            //NLog Algorithm Results
            //TODO

            //Send algorithm email
            if (parameters.SendAlgorithmEmail)
            {
                
                var baseCurrencyPriceUSDT = _iInformationService.GetPrice(parameters.BaseCurrency + "USDT");
                var emailBody = _iEmailService.GetREBALANCEEmailBody(parameters.BaseCurrency, balances, trades);

                Console.WriteLine("REBALANCE Algorithm Complete");
                Console.WriteLine(emailBody);
                string subject = "REBALANCE Algo Results";
                if(trades != null && trades.Any())
                { subject += " - TRADES FOUND"; }//if

                subject += " - ID " + RunID.ToString();

                _iEmailService.SendEmail(parameters.InformationEmailAddress, parameters.BATsEmailAddress,
                        parameters.BATsEmailPW, parameters.SMTPServer, subject, emailBody);

            }//if


            return trades;
        }//ExecuteAlgorithm

        public void ExecuteTrades(List<Trade> trades, Parameters parameters)
        {
            try
            {
                foreach (var trade in trades)
                {
                    _iTradeService.ExecuteTrade(trade, parameters.BaseCurrency, parameters.APIKey, parameters.APITradingKey, parameters.TestTrades);
                }//foreach

                //Log File Results
                //TODO

                //Send results email
                _iEmailService.SendEmail(parameters.InformationEmailAddress, parameters.BATsEmailAddress,
                        parameters.BATsEmailPW, parameters.SMTPServer, "REBALANCE Trade Results - ID " + RunID.ToString(), "All trades executed successfully!");
                Console.WriteLine("All trades executed successfully!");
               
            }//try
            catch (Exception e)
            {
                StringBuilder sb = new StringBuilder();
                //Log File Results
                string errorMessages = e.Message + "<br/>";

                Exception loopEx = e;

                while (loopEx.InnerException != null)
                {
                    loopEx = loopEx.InnerException;
                    errorMessages += loopEx.Message + "<br/>";
                }//while 

                //"Aborting run!" is the key phrase used to by email rules and other sections of code to indicate that a fatal error occurred.
                //Do not remove the phrase unless refactoring in all other places.
                sb.AppendLine("<b> An error occurred during trading!</b> <br /> <i>Message</i>: " + errorMessages);
                sb.AppendLine("<br />");

                _iEmailService.SendEmail(parameters.InformationEmailAddress, parameters.BATsEmailAddress,
                       parameters.BATsEmailPW, parameters.SMTPServer, "REBALANCE Trade Results - ERROR - ID " + RunID.ToString(), sb.ToString());

                Console.WriteLine(sb.ToString());
            }//catch                           
            
        }//ExecuteTrades

    }//Program
}
