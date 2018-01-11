using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAT_Models.Exceptions
{
    /// <summary>
    /// An exception to use for handled API information retrieval errors.
    /// </summary>
    public class InformationException : Exception
    {
        public InformationException(): base()
        {

        }//Constructor

        public InformationException(string message): base(message)
        {

        }//Constructor

    }//ConfigurationException
}
