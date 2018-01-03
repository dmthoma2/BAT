using BAT_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAT_Services
{
    public interface IParametersService
    {
        Parameters GetConfigurationSettings();
    }//IParametersService

    /// <summary>
    /// Provides methods associated with retrieving configuration parameters.
    /// </summary>
    public class ParametersService : IParametersService
    {
        /// <summary>
        /// Retrieves all configuration settings in one swoop.
        /// </summary>
        public Parameters GetConfigurationSettings()
        {
            return new Parameters();
        }//GetConfigurationSettings


    }//ParametersService
}
