using FizzWare.NBuilder;
using MortgageCalculator.Core.Models;
using MortgageCalculator.Core.Providers;
using Xunit;

namespace MortgageCalculator.Core.UnitTests
{
    public class EligibilityCheckProviderTests
    {
        [Theory]
        [InlineData(2000, true)]
        [InlineData(8000, false)]
        public void GetEligibilityCheckForMortgageNotExceedFourTimesIncome(double monthlyMortgageCost, bool expected)
        {
            var mortgageData = Builder<MortgageInput>.CreateNew()
                .With(x => x.MaturityPeriod = 2)
                .And(x => x.LoanValueAmount = 300000)
                .And(x => x.HomeValueAmount = 350000)
                .And(x => x.IncomeAmount = 18000).Build();
            var eligibilityProvider = new EligibilityCheckProvider();
            var actual = eligibilityProvider.IsEligibleForMortgage(monthlyMortgageCost, mortgageData);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2000, 350000, true)]
        [InlineData(8000, 250000, false)]
        public void GetEligibilityCheckForMortgageNotExceedHomeValue(double monthlyMortgageCost, double homeValue, bool expected)
        {
            var mortgageData = Builder<MortgageInput>.CreateNew()
                .With(x => x.MaturityPeriod = 2)
                .And(x => x.LoanValueAmount = 300000)
                .And(x => x.HomeValueAmount = homeValue)
                .And(x => x.IncomeAmount = 18000).Build();
            var eligibilityProvider = new EligibilityCheckProvider();
            var actual = eligibilityProvider.IsEligibleForMortgage(monthlyMortgageCost, mortgageData);
            Assert.Equal(expected, actual);
        }
    }
}
