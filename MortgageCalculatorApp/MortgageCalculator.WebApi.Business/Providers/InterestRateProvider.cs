using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MortgageCalculator.Data;
using MortgageCalculator.Data.Models;

namespace MortgageCalculator.Core.Providers
{
    public class InterestRateProvider : IInterestRateProvider
    {
        private readonly IMortgageCalculatorRepository _repository;
        public InterestRateProvider(IMortgageCalculatorRepository repository)
        {
            _repository = repository;
        }
        public double GetInterestRateForMaturityPeriod(int maturityPeriod)
        {
            return  _repository.GetMortgageRatesAsync().Result
                .Where(z => z.MaturityPeriod == maturityPeriod)
                .Select(z => z.InterestRate).FirstOrDefault();
        }

        public async Task<IEnumerable<MortgageRate>> GetMortgageRates()
        {
            return await _repository.GetMortgageRatesAsync();
        }
    }
}
