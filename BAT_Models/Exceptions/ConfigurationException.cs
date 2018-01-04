using System;
using System.Collections.Generic;
using System.Text;

namespace BAT_Models.Exceptions
{
    /// <summary>
    /// An exception to use for handled configuration parsing errors.
    /// </summary>
    public class ConfigurationException : Exception
    {
        public ConfigurationException(): base()
        {

        }//Constructor

        public ConfigurationException(string message): base(message)
        {

        }//Constructor

    }//ConfigurationException
}
