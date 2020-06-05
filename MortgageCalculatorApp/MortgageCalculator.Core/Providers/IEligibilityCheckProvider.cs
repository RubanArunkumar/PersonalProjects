using MortgageCalculator.Core.Models;

namespace MortgageCalculator.Core.Providers
{
    public interface IEligibilityCheckProvider
    { 
        /// <summary>
        /// Method to validate the eligibility for the mortgage
        /// </summary>
        /// <param name="monthlyMortgageCost"></param>
        /// <param name="mortgageInput"></param>
        /// <returns>the bool value.</returns>
        bool IsEligibleForMortgage(double monthlyMortgageCost, MortgageInput mortgageInput);
    }
}
