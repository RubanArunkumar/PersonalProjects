using System;
using Microsoft.Extensions.DependencyInjection;
using MortgageCalculator.Data.Models;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MortgageCalculator.Data.UnitTests
{
    public class MortgageCalculatorRepositoryTests
    {
        [Fact]
        public void GetAllMortgageRatesReturnsValue()
        {
            using (var serviceProvider = BuildServiceProvider())
            {
                using (var dbContext = serviceProvider.GetService<MortgageCalculatorDbContext>())
                {
                    dbContext.MortgageRateDetails.Add(new MortgageRate() { MaturityPeriod = 1, InterestRate = 1.2, LastUpdatedTime = DateTime.Now });
                    dbContext.MortgageRateDetails.Add(new MortgageRate() { MaturityPeriod = 2, InterestRate = 1.5, LastUpdatedTime = DateTime.Now.AddHours(1) });
                    dbContext.SaveChanges();
                    Assert.True(dbContext.MortgageRateDetails.Count() == 2);
                }
            }
        }

        [Fact]
        public void GetAllMortgageRatesReturnsEmptyList()
        {
            using (var serviceProvider = BuildServiceProvider())
            {
                using (var dbContext = serviceProvider.GetService<MortgageCalculatorDbContext>())
                {
                    dbContext.MortgageRateDetails.Add(new MortgageRate());
                    dbContext.SaveChanges();
                    Assert.True(dbContext.MortgageRateDetails.Any());
                }
            }
        }

        private static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddDbContext<MortgageCalculatorDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);

            return services.BuildServiceProvider();
        }
    }
}
