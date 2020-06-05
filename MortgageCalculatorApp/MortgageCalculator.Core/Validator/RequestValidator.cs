using MortgageCalculator.Core.Extension;
using MortgageCalculator.Core.Models;

namespace MortgageCalculator.Core.Validator
{
    public class RequestValidator : IRequestValidator
    {
        public bool ValidateMortgageCalculateRequest(MortgageInput mortgageInput)
        {
            return mortgageInput.IsValidMaturityPeriodInput();
        }
    }
}
