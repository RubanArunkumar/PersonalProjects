using FizzWare.NBuilder;
using Moq;
using MortgageCalculator.Core.Models;
using MortgageCalculator.Core.Providers;
using Xunit;

namespace MortgageCalculator.Core.UnitTests
{
    public class MortgageCalculatorProviderTests
    {
        private const double InterestRate = 2.5;

        private readonly Mock<IInterestRateProvider> _interestRateProviderMock;

        private readonly Mock<IEligibilityCheckProvider> _eligibilityProviderMock;

        public MortgageCalculatorProviderTests()
        {
            _interestRateProviderMock = new Mock<IInterestRateProvider>();
            _eligibilityProviderMock = new Mock<IEligibilityCheckProvider>();
            _interestRateProviderMock.Setup(x => x.GetInterestRateForMaturityPeriod(It.IsAny<int>())).Returns(InterestRate);
            _eligibilityProviderMock.Setup(x => x.IsEligibleForMortgage(It.IsAny<double>(), It.IsAny<MortgageInput>()))
                .Returns(true);
        }

        [Fact]
        public void GetMonthlyMortgageCalculationReturnsValue()
        {
            const double expectedResult = 4.28;
            var mortgageData = Builder<MortgageInput>.CreateNew()
                .With(x => x.MaturityPeriod = 2)
                .And(x => x.LoanValueAmount = 100)
                .And(x => x.HomeValueAmount = 80)
                .And(x => x.IncomeAmount = 10).Build();
            var mortgageCalculateProvider = new MortgageCalculateProvider(_interestRateProviderMock.Object, _eligibilityProviderMock.Object);
            var actualResult = mortgageCalculateProvider.GetMortgageResult(mortgageData);
            Assert.Equal(expectedResult, actualResult.MonthlyCostAmount);
            _eligibilityProviderMock.Verify(x => x.IsEligibleForMortgage(It.IsAny<double>(), It.IsAny<MortgageInput>()), Times.AtLeastOnce);
            _interestRateProviderMock.Verify(x => x.GetInterestRateForMaturityPeriod(It.IsAny<int>()), Times.AtLeastOnce);

        }
    }
}
