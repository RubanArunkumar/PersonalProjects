using System.Collections.Generic;
using AutoMapper;
using MortgageCalculator.Core.Models;
using MortgageCalculator.Data.Models;
using MortgageCalculator.WebApi.Models;

namespace MortgageCalculator.WebApi.Mapper
{
    public class ApiToCoreMapper : Profile
    {
        public ApiToCoreMapper()
        {
            CreateMap<MortgageCalculateRequest, MortgageInput>()
                .ForMember(dst => dst.MaturityPeriod, opt => opt.MapFrom(src => src.MaturityPeriod))
                .ForMember(dst => dst.LoanValueAmount, opt => opt.MapFrom(src => src.LoanValueAmount))
                .ForMember(dst => dst.HomeValueAmount, opt => opt.MapFrom(src => src.HomeValueAmount))
                .ForMember(dst => dst.IncomeAmount, opt => opt.MapFrom(src => src.IncomeAmount));

            CreateMap<MortgageResult, MortgageCalculateResponse>()
                .ForMember(dst => dst.MonthlyCostAmount, opt => opt.MapFrom(src => src.MonthlyCostAmount))
                .ForMember(dst => dst.MortgageEligibility, opt => opt.MapFrom(src => src.MortgageEligibility));
        }
    }
}
