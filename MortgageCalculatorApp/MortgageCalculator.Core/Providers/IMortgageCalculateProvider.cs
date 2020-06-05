using MortgageCalculator.Core.Models;

namespace MortgageCalculator.Core.Providers
{
    public interface IMortgageCalculateProvider
    {
        /// <summary>
        /// The method to get the mortgage result model.
        /// </summary>
        /// <param name="mortgageInput"></param>
        /// <returns></returns>
        MortgageResult GetMortgageResult(MortgageInput mortgageInput);
    }
}
