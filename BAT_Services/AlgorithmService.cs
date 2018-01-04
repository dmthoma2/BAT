using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAT_Models.API;
using BAT_Models.Algorithm;

namespace BAT_Services
{
    public interface IAlgorithmService
    {
        List<Trade> ExecuteREBALANCE(REBALANCE_Params parameters);
    }//IAlgorithmService

    /// <summary>
    /// This service contains various trading algorithm implemntations.
    /// </summary>
    public class AlgorithmService : IAlgorithmService
    {
        public List<Trade> ExecuteREBALANCE(REBALANCE_Params parameters)
        {
            //TODO

            return new List<Trade>();
        }//ExecuteREBALANCE

    }//AlgorithmService
}
