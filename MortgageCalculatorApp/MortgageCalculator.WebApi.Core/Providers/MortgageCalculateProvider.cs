using System;
using MortgageCalculator.Core.Models;

namespace MortgageCalculator.Core.Providers
{
    public class MortgageCalculateProvider : IMortgageCalculateProvider
    {
        private readonly IInterestRateProvider _interestRateProvider;
        private readonly IEligibilityCheckProvider _eligibilityCheckProvider;

        public MortgageCalculateProvider(IInterestRateProvider interestRateProvider, 
            IEligibilityCheckProvider eligibilityCheckProvider)
        {
            _interestRateProvider = interestRateProvider;
            _eligibilityCheckProvider = eligibilityCheckProvider;
        }

        public MortgageResult GetMortgageResult(MortgageInput mortgageInput)
        {
            var monthlyMortgageResult = MonthlyMortgageCalculate(mortgageInput.LoanValueAmount, mortgageInput.MaturityPeriod);
            var isEligible = _eligibilityCheckProvider.IsEligibleForMortgage(monthlyMortgageResult, mortgageInput);
            return new MortgageResult(monthlyMortgageResult, isEligible);
        }

        private double MonthlyMortgageCalculate(double loanValueAmount, int maturityPeriod)
        {
            var interestRateInPercentage = _interestRateProvider.GetInterestRateForMaturityPeriod(maturityPeriod);
            var monthlyInterestRate = (interestRateInPercentage / 100) / 12;
            var maturityPeriodInMonths = maturityPeriod * 12;
            var mortgageAmount = loanValueAmount * ((monthlyInterestRate * Math.Pow((1 + monthlyInterestRate), maturityPeriodInMonths)) / (Math.Pow((1 + monthlyInterestRate), maturityPeriodInMonths) - 1 ));
            return Math.Round(mortgageAmount, 2);
        }
    }
}
