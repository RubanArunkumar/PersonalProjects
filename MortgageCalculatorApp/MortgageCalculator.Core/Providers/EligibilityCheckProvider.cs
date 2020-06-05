using MortgageCalculator.Core.Models;

namespace MortgageCalculator.Core.Providers
{
    public class EligibilityCheckProvider : IEligibilityCheckProvider
    {
        public bool IsEligibleForMortgage(double monthlyMortgageCost, MortgageInput mortgageInput)
        {
            var totalMortgageCost = monthlyMortgageCost * 12 * mortgageInput.MaturityPeriod;
            return IsTotalMortgageNotExceedHomeValue(totalMortgageCost, mortgageInput.HomeValueAmount) &&
                   IsMortgageNotExceedIncome(totalMortgageCost, mortgageInput.IncomeAmount);
        }

        private bool IsMortgageNotExceedIncome(double totalMortgageCost, double incomeAmount)
        {
            return (totalMortgageCost < (incomeAmount * 4));
        }

        private bool IsTotalMortgageNotExceedHomeValue(double totalMortgageCost, double homeValueAmount)
        {
            return (totalMortgageCost < homeValueAmount);
        }
    }
}
