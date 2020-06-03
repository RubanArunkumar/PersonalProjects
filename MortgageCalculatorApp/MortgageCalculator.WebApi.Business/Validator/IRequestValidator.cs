using MortgageCalculator.Core.Models;

namespace MortgageCalculator.Core.Validator
{
    public interface IRequestValidator
    {
        bool ValidateMortgageCalculateRequest(MortgageInput mortgageInput);
    }
}
