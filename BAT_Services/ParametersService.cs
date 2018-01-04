using BAT_Models;
using BAT_Models.Exceptions;
using System;
using BAT_Utilities;
using System.Collections.Generic;
using System.Text;

namespace BAT_Services
{
    public interface IParametersService
    {
        Parameters GetConfigurationSettings();
        Parameters GetAPIInformation(Parameters parameters);
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
            Parameters output = new Parameters();
            try
            {
                output.SuccessfullyPopulated = true;
                output.ErrorMessage = string.Empty;

                output = GetAPIInformation(output);
            }
            catch(ConfigurationException ce)
            {
                output = new Parameters();
                output.SuccessfullyPopulated = false;
                output.ErrorMessage = "A known issue occurred while parsing configuration settings.  " +
                    "Please review the error message and confirm the settings are correct and restart the application.  Message: " + ce.Message;
            }//ConfigurationException
            catch(Exception ex)
            {
                output = new Parameters();
                output.SuccessfullyPopulated = false;
                output.ErrorMessage = "A unknown issue occurred while parsing configuration settings.  Message: " + ex.Message;
            }//Exception


            return output;
        }//GetConfigurationSettings

        /// <summary>
        /// Retrieves API related information from the app settings.
        /// </summary>
        public Parameters GetAPIInformation(Parameters parameters)
        {
            parameters.BinanceAPIAddress = string.Empty;

            return parameters;
        }//GetAPIInformation


    }//ParametersService
}
