using MortgageCalculator.Core.Models;

namespace MortgageCalculator.Core.Extension
{
    public static class InputRequestExtension
    {
        public static bool IsValidMaturityPeriodInput(this MortgageInput value)
        {
            return value.MaturityPeriod >= 1 && value.MaturityPeriod <= 12;
        }
    }
}
