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
        Parameters GetNotificationSettings(Parameters parameters);
        Parameters GetAlgorithmType(Parameters parameters);


        Parameters GetBaseCurrencyInformation(Parameters parameters);
        Parameters GetCurrency1Information(Parameters parameters);
        Parameters GetCurrency2Information(Parameters parameters);
        Parameters GetCurrency3Information(Parameters parameters);
        Parameters GetCurrency4Information(Parameters parameters);
        Parameters GetCircuitBreakerInformation(Parameters parameters);
        void VerifyREBALANCETotals(Parameters parameters);

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
                output = GetCircuitBreakerInformation(output);
                output = GetNotificationSettings(output);
                output = GetAlgorithmType(output);
                
                if(output.Algo == Parameters.TradingAlgorithms.REBALANCE)
                {
                    output = GetBaseCurrencyInformation(output);
                    output = GetCurrency1Information(output);
                    output = GetCurrency2Information(output);
                    output = GetCurrency3Information(output);
                    output = GetCurrency4Information(output);
                    VerifyREBALANCETotals(output);
                }//if
                
                //TODO Place new algorithms configuration settings here

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
            { throw new ConfigurationException("No APIKey supplied.  A key must be provided to pass security credentials with Binance."); }//if

            parameters.APITradingKey = _appSettings.APITradingKey();

            if (string.IsNullOrWhiteSpace(parameters.APITradingKey))
            { throw new ConfigurationException("No APITradingKey supplied.  A key must be provided to authorize trading and account access with Binance."); }//if


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
        /// Retrieves Currency1 information from the app settings for "REBALANCE" trading.
        /// </summary>
        public Parameters GetCurrency1Information(Parameters parameters)
        {
            parameters.Currency1 = _appSettings.Currency1();
            if (string.IsNullOrWhiteSpace(parameters.Currency1))
            { throw new ConfigurationException("No Currency1 supplied.  This is a required field for rebalancing."); }//if

            parameters.Currency1Allocation = _appSettings.Currency1Allocation();
            if (parameters.Currency1Allocation < 10 || parameters.Currency1Allocation > 90)
            { throw new ConfigurationException("Currency1 must have an allocation between 10 and 90."); }//if

            parameters.Currency1InitialAllocation = _appSettings.Currency1InitialAllocation();
            if (parameters.Currency1InitialAllocation < 0)
            { parameters.Currency1InitialAllocation = 0; }//if

            return parameters;
        }//GetCurrency1Information

        /// <summary>
        /// Retrieves Currency2 information from the app settings for "REBALANCE" trading.
        /// </summary>
        public Parameters GetCurrency2Information(Parameters parameters)
        {
            parameters.Currency2 = _appSettings.Currency2();
            if (string.IsNullOrWhiteSpace(parameters.Currency2))
            {
                parameters.Currency2 = string.Empty;
                return parameters;
            }//if

            parameters.Currency2Allocation = _appSettings.Currency2Allocation();
            if (parameters.Currency2Allocation < 10 || parameters.Currency2Allocation > 90)
            { throw new ConfigurationException("Currency2 must have an allocation between 10 and 90."); }//if

            parameters.Currency2InitialAllocation = _appSettings.Currency2InitialAllocation();
            if (parameters.Currency2InitialAllocation < 0)
            { parameters.Currency2InitialAllocation = 0; }//if

            return parameters;
        }//GetCurrency2Information

        /// <summary>
        /// Retrieves Currency3 information from the app settings for "REBALANCE" trading.
        /// </summary>
        public Parameters GetCurrency3Information(Parameters parameters)
        {
            parameters.Currency3 = _appSettings.Currency3();
            if (string.IsNullOrWhiteSpace(parameters.Currency3))
            {
                parameters.Currency3 = string.Empty;
                return parameters;
            }//if

            parameters.Currency3Allocation = _appSettings.Currency3Allocation();
            if (parameters.Currency3Allocation < 10 || parameters.Currency3Allocation > 90)
            { throw new ConfigurationException("Currency3 must have an allocation between 10 and 90."); }//if

            parameters.Currency3InitialAllocation = _appSettings.Currency3InitialAllocation();
            if (parameters.Currency3InitialAllocation < 0)
            { parameters.Currency3InitialAllocation = 0; }//if

            return parameters;
        }//GetCurrency3Information

        /// <summary>
        /// Retrieves Currency4 information from the app settings for "REBALANCE" trading.
        /// </summary>
        public Parameters GetCurrency4Information(Parameters parameters)
        {
            parameters.Currency4 = _appSettings.Currency4();
            if (string.IsNullOrWhiteSpace(parameters.Currency4))
            {
                parameters.Currency4 = string.Empty;
                return parameters;
            }//if

            parameters.Currency4Allocation = _appSettings.Currency4Allocation();
            if (parameters.Currency4Allocation < 10 || parameters.Currency4Allocation > 90)
            { throw new ConfigurationException("Currency4 must have an allocation between 10 and 90."); }//if

            parameters.Currency4InitialAllocation = _appSettings.Currency4InitialAllocation();
            if (parameters.Currency4InitialAllocation < 0)
            { parameters.Currency4InitialAllocation = 0; }//if

            return parameters;
        }//GetCurrency4Information

        /// <summary>
        /// Retrieves circuit breaker information from the app settings.
        /// </summary>
        public Parameters GetCircuitBreakerInformation(Parameters parameters)
        {
            parameters.UseCircuitBreaker = _appSettings.UseCircuitBreaker();
            parameters.CircuitBreakerTrades = _appSettings.CircuitBreakerTrades();

            if(parameters.CircuitBreakerTrades < 1)
            { parameters.CircuitBreakerTrades = 1; }//if

            parameters.CircuitBreakerHours = _appSettings.CircuitBreakerHours();

            if(parameters.CircuitBreakerHours < 1)
            { parameters.CircuitBreakerHours = 1; }//if
            
            return parameters;
        }//GetCircuitBreakerInformation

        /// <summary>
        /// Retrieves emailing notification settings from the app settings.
        /// </summary>
        public Parameters GetNotificationSettings(Parameters parameters)
        {
            parameters.BATsEmailAddress = _appSettings.BATsEmailAddress();

            if (string.IsNullOrWhiteSpace(parameters.BATsEmailAddress) || !parameters.BATsEmailAddress.Contains("@"))
            { parameters.BATsEmailAddress = "BinanceAutoTrader@gmail.com"; }//if

            parameters.BATsEmailPW = _appSettings.BATsEmailPW();
            if (string.IsNullOrWhiteSpace(parameters.BATsEmailPW))
            { parameters.BATsEmailPW = "ABC123DEF" ; }//if

            parameters.SMTPServer = _appSettings.SMTPServer();

            if (string.IsNullOrWhiteSpace(parameters.SMTPServer))
            { parameters.SMTPServer = "smtp.gmail.com"; }//if

            parameters.InformationEmailAddress = _appSettings.InformationEmailAddress();                  
            parameters.SendLoadingEmail = _appSettings.SendLoadingEmail();
            parameters.SendAlgorithmEmail = _appSettings.SendAlgorithmEmail();
            parameters.SendTradeExecutionEmail = _appSettings.SendTradeExecutionEmail();

            if (string.IsNullOrWhiteSpace(parameters.InformationEmailAddress))
            {
                if (parameters.SendLoadingEmail || parameters.SendAlgorithmEmail || parameters.SendTradeExecutionEmail)
                { throw new ConfigurationException("An email address is required to send emails."); }//if
                else
                { parameters.InformationEmailAddress = string.Empty; }//else
            }//if

            parameters.BuyAndHoldComparison = _appSettings.BuyAndHoldComparison();
            parameters.FailOnError = _appSettings.FailOnError();

            parameters.HistoryFile = _appSettings.HistoryFile();

            if (string.IsNullOrWhiteSpace(parameters.HistoryFile))
            { throw new ConfigurationException("A history file location is required."); }//if
            
            return parameters;
        }//GetNotificationSettings

        /// <summary>
        /// Parses the algorithm type to be used.
        /// </summary>
        public Parameters GetAlgorithmType(Parameters parameters)
        {
            var algo = _appSettings.Algo();
            if (algo != null)
            { parameters.Algo = algo.Trim().ToUpper(); }//if

            if(parameters.Algo != Parameters.TradingAlgorithms.REBALANCE)
            {
                throw new ConfigurationException("Unrecognized trading algorithm.");
            }//if
            
            return parameters;
        }//GetAlgorithmType

        /// <summary>
        /// Ensures that individual allocation totals sum up to 100%.
        /// </summary>
        public void VerifyREBALANCETotals(Parameters parameters)
        {
            int AllocationTotal = parameters.BaseCurrencyAllocation + parameters.Currency1Allocation +
                parameters.Currency2Allocation + parameters.Currency3Allocation + 
                parameters.Currency4Allocation;

            if(AllocationTotal != 100)
            {
                throw new ConfigurationException("Total allocation of all currencies must equal 100!.");
            }//if
                       
        }//VerifyREBALANCETotals

    }//ParametersService
}
