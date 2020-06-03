using System;
using System.ComponentModel.DataAnnotations;

namespace MortgageCalculator.Data.Models
{
    public class MortgageRate
    {
        /// <summary>
        /// The property to get or set the Maturity period
        /// </summary>
        [Key]
        public int MaturityPeriod { get; set; }

        /// <summary>
        /// The property to get or set the Interest Rate in percentage
        /// </summary>
        public double InterestRate { get; set; }

        /// <summary>
        /// The property to get or set the last updated time
        /// </summary>
        public DateTime LastUpdatedTime { get; set; }
    }
}
