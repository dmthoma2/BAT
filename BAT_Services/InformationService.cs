using System;
using System.Collections.Generic;
using System.Text;
using BAT_Models.Exceptions;
using BAT_Repository;

namespace BAT_Services
{
    public interface IInformationService
    {
        decimal GetPrice(string Symbol);
    }//IInformationService

    /// <summary>
    /// This service returns current market values and account balance information.
    /// </summary>
    public class InformationService : IInformationService
    {
        private IAPIRepository _apiRepository;

        public InformationService(IAPIRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }//InformationService

        public decimal GetPrice(string Symbol)
        {
            if (string.IsNullOrWhiteSpace(Symbol))
            {
                throw new InformationException("Cannot retrieve pricing information with a null/empty symbol");
            }//if

            var price = _apiRepository.GetPrice(Symbol);

            return price;
        }//GetPrice

    }//InformationService
}
