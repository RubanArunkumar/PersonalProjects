using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace MortgageCalculator.Data.Models
{
    public static class DataModelGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MortgageCalculatorDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<MortgageCalculatorDbContext>>()))
            {
                // Look for any available interest rates.
                if (context.MortgageRateDetails.Any())
                {
                    return;   // Data was already seeded
                }

                context.MortgageRateDetails.AddRange(
                    new MortgageRate
                    {
                        MaturityPeriod = 1,
                        InterestRate = 2.19,
                        LastUpdatedTime = DateTime.Now.AddDays(-1)
                    },
                    new MortgageRate
                    {
                        MaturityPeriod = 2,
                        InterestRate = 2.09,
                        LastUpdatedTime = DateTime.Now.AddDays(-2)
                    },
                    new MortgageRate
                    {
                        MaturityPeriod = 3,
                        InterestRate = 2.14,
                        LastUpdatedTime = DateTime.Now.AddDays(-3)
                    },
                    new MortgageRate
                    {
                        MaturityPeriod = 4,
                        InterestRate = 2.49,
                        LastUpdatedTime = DateTime.Now.AddDays(-4)
                    },
                    new MortgageRate
                    {
                        MaturityPeriod = 5,
                        InterestRate = 2.54,
                        LastUpdatedTime = DateTime.Now.AddDays(-5)
                    });
                context.SaveChanges();
            }
            
        }
    }
}
