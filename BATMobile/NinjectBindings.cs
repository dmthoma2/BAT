using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using BAT_Services;
using BAT_Utilities;

namespace BATMobile
{
    public class NinjectBindings : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IAppSettings>().To<AppSettings>();

            Bind<IParametersService>().To<ParametersService>();            
            Bind<IAlgorithmService>().To<AlgorithmService>();
            Bind<IInformationService>().To<InformationService>();
            Bind<ITradeService>().To<TradeService>();
        }
    }//NinjectBindings
}
