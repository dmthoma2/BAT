using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAT_Models.Exceptions
{
    /// <summary>
    /// An exception indicating that a error was handling while writing log records.
    /// </summary>
    public class LogException : Exception
    {
        public LogException() : base()
        {

        }//Constructor

        public LogException(string message) : base(message)
        {

        }//Constructor
    }//LogException
}
