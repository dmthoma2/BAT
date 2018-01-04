using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAT_Services;
using Ninject;
using System.Reflection;

namespace BATMobile
{
    /// <summary>
    /// BATMobile is a automated Binance API cryptocurrency trading application.
    /// It is configuration driven and suppored by a SOA for trading logic and trade execution.
    /// </summary>
    public class Program
    {
        private IParametersService _iParametersService;

        public Program(IParametersService iParametersService)
        {
            _iParametersService = iParametersService;
        }

        static void Main(string[] args)
        {
            StandardKernel _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());
            
            Program prog = new Program(_kernel.Get<IParametersService>());

            //Parse configuration into a model
            var parameters = prog._iParametersService.GetConfigurationSettings();

            //Call trading algorithm


            //Execute trades based on results

        }//Main
    }//Program
}
