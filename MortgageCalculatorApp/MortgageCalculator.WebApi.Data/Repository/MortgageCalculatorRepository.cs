using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MortgageCalculator.Data.Models;

namespace MortgageCalculator.Data.Repository
{
    public class MortgageCalculatorRepository : IMortgageCalculatorRepository
    {
        private readonly MortgageCalculatorDbContext _dbContext;
        public MortgageCalculatorRepository(MortgageCalculatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<MortgageRate>> GetMortgageRatesAsync()
        {
            return await _dbContext.MortgageRateDetails.ToListAsync();
        }
    }
}
