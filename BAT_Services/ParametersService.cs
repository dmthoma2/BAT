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
        Parameters GetBaseCurrencyInformation(Parameters parameters);
        Parameters GetCurrencyInformation(Parameters parameters);
        Parameters GetCircuitBreakerInformation(Parameters parameters);
        Parameters GetNotificationSettings(Parameters parameters);
        Parameters VerifyREBALANCETotals(Parameters parameters);

    }//IParametersService

    /// <summary>
    /// Provides methods associated with retrieving configuration parameters.
    /// </summary>
    public class ParametersService : IParametersService
    {
        IAppSettings _appSettings;

        public ParametersService(IAppSettings iAppSettings)
        {
            _appSettings = iAppSettings;
        }//Constructor

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
                output = GetBaseCurrencyInformation(output);
                output = GetCurrencyInformation(output);
                output = GetCircuitBreakerInformation(output);
                output = GetNotificationSettings(output);

            }//try
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
            parameters.BinanceAPIAddress = _appSettings.BinanceAPIAddress();

            if (string.IsNullOrWhiteSpace(parameters.BinanceAPIAddress))
            { throw new ConfigurationException("No BinanceAPIAddress supplied.  A URL must be provided to connect."); }//if

            parameters.APIKey = _appSettings.APIKey();

            if (string.IsNullOrWhiteSpace(parameters.APIKey))
            { throw new ConfigurationException("No APIKey supplied.  A key must be provided to pass security crednetials with Binance."); }//if
            
            return parameters;
        }//GetAPIInformation

        /// <summary>
        /// Retrieves base currency information from the app settings for "REBALANCE" trading.
        /// </summary>
        public Parameters GetBaseCurrencyInformation(Parameters parameters)
        {
            parameters.RebalanceThreshold = _appSettings.RebalanceThreshold();

            if(parameters.RebalanceThreshold < 5)
            { throw new ConfigurationException("Reblance threashold has a minimum value of 5."); }//if

            parameters.BaseCurrency = _appSettings.BaseCurrency();
            if (string.IsNullOrWhiteSpace(parameters.BaseCurrency))
            { throw new ConfigurationException("No base currency supplied.  This is a required field for rebalancing."); }//if

            parameters.BaseCurrencyAllocation = _appSettings.BaseCurrencyAllocation();
            if(parameters.BaseCurrencyAllocation < 10 || parameters.BaseCurrencyAllocation > 90)
            { throw new ConfigurationException("Base Currency must have an allocation between 10 and 90."); }//if

            parameters.BaseCurrencyInitialAllocation = _appSettings.BaseCurrencyInitialAllocation();
            if (parameters.BaseCurrencyInitialAllocation < 0 )
            { parameters.BaseCurrencyInitialAllocation = 0; }//if
            
            return parameters;
        }//GetBaseCurrencyInformation

        /// <summary>
        /// Retrieves currency information from the app settings for "REBALANCE" trading.
        /// </summary>
        public Parameters GetCurrencyInformation(Parameters parameters)
        {
            //TODO
            return parameters;
        }//GetCurrencyInformation

        /// <summary>
        /// Retrieves circuit breaker information from the app settings.
        /// </summary>
        public Parameters GetCircuitBreakerInformation(Parameters parameters)
        {
            //TODO
            return parameters;
        }//GetCircuitBreakerInformation

        /// <summary>
        /// Retrieves emailing notification settings from the app settings.
        /// </summary>
        public Parameters GetNotificationSettings(Parameters parameters)
        {
            //TODO
            return parameters;
        }//GetNotificationSettings

        /// <summary>
        /// Ensures that individual allocation totals sum up to 100%.
        /// </summary>
        public Parameters VerifyREBALANCETotals(Parameters parameters)
        {
            //TODO
            return parameters;
        }//VerifyREBALANCETotals

    }//ParametersService
}
