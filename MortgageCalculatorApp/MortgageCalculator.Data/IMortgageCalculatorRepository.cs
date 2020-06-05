using System.Collections.Generic;
using System.Threading.Tasks;
using MortgageCalculator.Data.Models;

namespace MortgageCalculator.Data
{
    public interface IMortgageCalculatorRepository
    {
        Task<IEnumerable<MortgageRate>> GetMortgageRatesAsync();
    }
}
