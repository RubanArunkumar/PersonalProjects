
namespace MortgageCalculator.WebApi.Models
{
    public class MortgageCalculateResponse
    {
        /// <summary>
        /// The property to get or set the monthly cost amount
        /// </summary>
        public double MonthlyCostAmount { get; set; }

        /// <summary>
        /// The property to get or set the eligibility for the mortgage
        /// </summary>
        public bool MortgageEligibility { get; set; }
    }
}
