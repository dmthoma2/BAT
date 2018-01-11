using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using BAT_Services;
using BAT_Utilities;
using BAT_Repository;
using Binance.Api;

namespace BATMobile
{
    public class NinjectBindings : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IAppSettings>().To<AppSettings>();

            Bind<IFileIORepository>().To<FileIORepository>();

            Bind<IParametersService>().To<ParametersService>();            
            Bind<IAlgorithmService>().To<AlgorithmService>();
            Bind<IInformationService>().To<InformationService>();
            Bind<ITradeService>().To<TradeService>();
            Bind<ILogService>().To<LogService>();
            Bind<IEmailService>().To<EmailService>();

            Bind<IAPIRepository>().To<APIRepository>();
        }
    }//NinjectBindings
}
