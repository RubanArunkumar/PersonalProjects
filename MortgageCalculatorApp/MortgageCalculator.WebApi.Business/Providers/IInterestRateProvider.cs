using System.Collections.Generic;
using System.Threading.Tasks;
using MortgageCalculator.Data.Models;

namespace MortgageCalculator.Core.Providers
{
    public interface IInterestRateProvider
    {
        /// <summary>
        /// Method to get the Interest rate for the provided maturity period
        /// </summary>
        /// <param name="maturityPeriod"></param>
        /// <returns></returns>
        double GetInterestRateForMaturityPeriod(int maturityPeriod);

        /// <summary>
        /// Method to fetch interest rate from the data provider.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MortgageRate>> GetMortgageRates();
    }
}
