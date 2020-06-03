using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MortgageCalculator.Core.Models;
using MortgageCalculator.Core.Providers;
using MortgageCalculator.Data.Models;
using MortgageCalculator.WebApi.Controllers;
using MortgageCalculator.WebApi.Models;
using Xunit;

namespace MortgageCalculator.WebApi.UnitTests
{
    public class MortgageCalculateControllerTests
    {
        private readonly Mock<ILogger<MortgageCalculatorController>> _logger;

        private readonly Mock<IMapper> _mapper;

        private readonly Mock<IMortgageCalculateProvider> _mortgageCalculateProvider;

        private readonly Mock<IInterestRateProvider> _interestRateProvider;

        private readonly MortgageCalculatorController _controller;

        public MortgageCalculateControllerTests()
        {
            _logger = new Mock<ILogger<MortgageCalculatorController>>();
            _mapper = new Mock<IMapper>();
            _mortgageCalculateProvider = new Mock<IMortgageCalculateProvider>();
            _interestRateProvider = new Mock<IInterestRateProvider>();
            _controller = new MortgageCalculatorController(
                _logger.Object,
                _mapper.Object,
                _mortgageCalculateProvider.Object,
                _interestRateProvider.Object);

            var mortgageRate = new List<MortgageRate>
            {
                new MortgageRate {InterestRate = 1.5, MaturityPeriod = 1, LastUpdatedTime = DateTime.Now}
            };
            _interestRateProvider.Setup(x => x.GetMortgageRates()).ReturnsAsync(mortgageRate);
        }

        [Fact]
        public void GetMethod_ShouldReturnMortgageRates()
        {
            var actionResult = _controller.GetInterestRates().Result;
            Assert.NotNull(actionResult);
            _interestRateProvider.Verify(x => x.GetMortgageRates(), Times.AtLeastOnce);
        }

        [Fact]
        public void PostMethod_ShouldCalculateMortgage()
        {
            var result = _controller.CalculateMortgageEligibility(new MortgageCalculateRequest
            {
                IncomeAmount = 45000,
                MaturityPeriod = 5,
                LoanValueAmount = 300000,
                HomeValueAmount = 250000
            });
            Assert.NotNull(result);
            _mortgageCalculateProvider.Verify(x=> x.GetMortgageResult(It.IsAny<MortgageInput>()), Times.AtLeastOnce);
        }
    }
}
