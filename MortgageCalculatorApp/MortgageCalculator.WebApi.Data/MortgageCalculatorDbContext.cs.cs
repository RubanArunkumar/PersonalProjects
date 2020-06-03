using Microsoft.EntityFrameworkCore;
using MortgageCalculator.Data.Models;

namespace MortgageCalculator.Data
{
    public class MortgageCalculatorDbContext : DbContext
    {
        public MortgageCalculatorDbContext(DbContextOptions<MortgageCalculatorDbContext> options) : base(options)
        {
        }

        public virtual DbSet<MortgageRate> MortgageRateDetails { get; set; }
    }
}
