namespace MortgageCalculator.Core.Models
{
    public class MortgageInput
    {
        /// <summary>
        /// The property to get or set the Income amount
        /// </summary>
        public double IncomeAmount { get; set; }

        /// <summary>
        /// The property to get or set the Maturity period
        /// </summary>
        public int MaturityPeriod { get; set; }

        /// <summary>
        /// The property to get or set the loan value amount
        /// </summary>
        public double LoanValueAmount { get; set; }

        /// <summary>
        /// The property to get or set the home value amount
        /// </summary>
        public double HomeValueAmount { get; set; }
    }
}
