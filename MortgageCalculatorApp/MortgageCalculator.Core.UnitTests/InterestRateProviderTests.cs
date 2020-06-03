using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MortgageCalculator.Core.Providers;
using MortgageCalculator.Data;
using MortgageCalculator.Data.Models;
using Xunit;

namespace MortgageCalculator.Core.UnitTests
{
    public class InterestRateProviderTests
    {
        private readonly Mock<IMortgageCalculatorRepository> _repository;

        private readonly InterestRateProvider _interestRateProvider;

        private readonly List<MortgageRate> _mortgageRate = new List<MortgageRate>
        {
            new MortgageRate {InterestRate = 1.5, MaturityPeriod = 1, LastUpdatedTime = DateTime.Now},
            new MortgageRate {InterestRate = 2.5, MaturityPeriod = 5, LastUpdatedTime = DateTime.Now}
        };
        public InterestRateProviderTests()
        {
            _repository = new Mock<IMortgageCalculatorRepository>();
            _interestRateProvider = new InterestRateProvider(_repository.Object);
            _repository.Setup(x => x.GetMortgageRatesAsync()).ReturnsAsync(_mortgageRate);
        }

        [Fact]
        public void GetInterestRateReturnsExpectedValue()
        {
            var result = _interestRateProvider.GetInterestRateForMaturityPeriod(5);
            Assert.Equal(result, _mortgageRate[1].InterestRate);

        }

        [Fact]
        public void GetInterestRatesFromDataBaseShouldReturnsValue()
        {
            _repository.Setup(x => x.GetMortgageRatesAsync()).ReturnsAsync(_mortgageRate);
            var result = _interestRateProvider.GetMortgageRates();
            _repository.Verify(x => x.GetMortgageRatesAsync(), Times.AtLeastOnce);
            Assert.True(result.Result.Any());
        }
    }
}
