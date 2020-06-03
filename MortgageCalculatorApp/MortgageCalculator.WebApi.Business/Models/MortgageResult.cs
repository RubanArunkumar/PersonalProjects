namespace MortgageCalculator.Core.Models
{
    public class MortgageResult
    {
        public MortgageResult(double monthlyCostAmount, bool mortgageEligibility)
        { 
            MonthlyCostAmount = monthlyCostAmount;
            MortgageEligibility = mortgageEligibility;
        }

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
