using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MortgageCalculator.Core.Models;
using MortgageCalculator.Core.Providers;
using MortgageCalculator.Core.Validator;
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

        private readonly Mock<IRequestValidator> _requestValidator;

        private readonly MortgageCalculatorController _controller;

        public MortgageCalculateControllerTests()
        {
            _logger = new Mock<ILogger<MortgageCalculatorController>>();
            _mapper = new Mock<IMapper>();
            _mortgageCalculateProvider = new Mock<IMortgageCalculateProvider>();
            _interestRateProvider = new Mock<IInterestRateProvider>();
            _requestValidator = new Mock<IRequestValidator>();
            _controller = new MortgageCalculatorController(
                _logger.Object,
                _mapper.Object,
                _mortgageCalculateProvider.Object,
                _interestRateProvider.Object,
                _requestValidator.Object);

            var mortgageResult = new MortgageResult(500, true);
            _mortgageCalculateProvider.Setup(x => x.GetMortgageResult(It.IsAny<MortgageInput>())).Returns(mortgageResult);
        }

        [Fact]
        public void GetMethod_ShouldReturnMortgageRates()
        {
            var actionResult = _controller.GetInterestRates().Result;
            Assert.NotNull(actionResult);
            _interestRateProvider.Verify(x => x.GetMortgageRates(), Times.AtLeastOnce);
        }

        [Fact]
        public void PostMethod_ShouldCalculateMortgageWithValidInput()
        {
            _requestValidator.Setup(x => x.ValidateMortgageCalculateRequest(It.IsAny<MortgageInput>())).Returns(true);
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

        [Fact]
        public void PostMethod_ShouldCalculateMortgageWithInValidInput()
        {
            _requestValidator.Setup(x => x.ValidateMortgageCalculateRequest(It.IsAny<MortgageInput>())).Returns(false);
            var result = _controller.CalculateMortgageEligibility(new MortgageCalculateRequest
            {
                IncomeAmount = 45000,
                MaturityPeriod = 14,
                LoanValueAmount = 300000,
                HomeValueAmount = 250000
            });
            Assert.NotNull(result);
            _requestValidator.Verify(x => x.ValidateMortgageCalculateRequest(It.IsAny<MortgageInput>()), Times.AtLeastOnce);
        }
    }
}
