using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using BAT_Services;

namespace BAT_Tests
{
    public class NinjectBindings : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IParametersService>().To<ParametersService>();
        }
    }//NinjectBindings
}
