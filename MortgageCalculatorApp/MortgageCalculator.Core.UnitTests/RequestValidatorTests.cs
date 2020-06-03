using FizzWare.NBuilder;
using MortgageCalculator.Core.Models;
using MortgageCalculator.Core.Validator;
using Xunit;

namespace MortgageCalculator.Core.UnitTests
{
    public class RequestValidatorTests
    {
        [Theory]
        [InlineData(5, true)]
        [InlineData(14, false)]
        public void IsValidRequestInput(int maturityPeriod, bool expectedResult)
        {
            var mortgageData = Builder<MortgageInput>.CreateNew()
                .With(x => x.MaturityPeriod = maturityPeriod)
                .And(x => x.LoanValueAmount = 300000)
                .And(x => x.HomeValueAmount = 350000)
                .And(x => x.IncomeAmount = 18000).Build();
            var requestValidator = new RequestValidator();
            var actualResult = requestValidator.ValidateMortgageCalculateRequest(mortgageData);
            Assert.Equal(expectedResult, actualResult);  
        }
    }
}
