using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MortgageCalculator.Core.Models;
using MortgageCalculator.Core.Providers;
using MortgageCalculator.Core.Validator;
using MortgageCalculator.WebApi.Models;

namespace MortgageCalculator.WebApi.Controllers
{
    [Route("")]
    public class MortgageCalculatorController : ControllerBase
    {
        private readonly ILogger<MortgageCalculatorController> _logger;
        private readonly IMapper _mapper;

        private readonly IMortgageCalculateProvider _mortgageCalculateProvider;
        private readonly IInterestRateProvider _interestRateProvider;
        private readonly IRequestValidator _requestValidator;

        public MortgageCalculatorController(
            ILogger<MortgageCalculatorController> logger,
            IMapper mapper,
            IMortgageCalculateProvider mortgageCalculateProvider,
            IInterestRateProvider interestRateProvider,
            IRequestValidator requestValidator)
        {
            _logger = logger;
            _mapper = mapper;
            _mortgageCalculateProvider = mortgageCalculateProvider;
            _interestRateProvider = interestRateProvider;
            _requestValidator = requestValidator;
        }

        [Route("/api/interest-rates")]
        [HttpGet]
        public async Task<IActionResult> GetInterestRates()
        {
            _logger.LogInformation("The method to get Interest rates is called");

            var responseResult = await _interestRateProvider.GetMortgageRates();
            return new JsonResult(responseResult);
        }

        [Route("/api/mortgage-check")]
        [HttpPost]
        public IActionResult CalculateMortgageEligibility([FromBody] MortgageCalculateRequest mortgageCalculateRequest)
        {
            _logger.LogInformation("The method to get calculate mortgage is called");
            var validatedResult = _requestValidator.ValidateMortgageCalculateRequest(_mapper.Map<MortgageInput>(mortgageCalculateRequest));

            if (!validatedResult)
            {
                return new BadRequestObjectResult("Invalid User Input");
            }

            var result =
                _mortgageCalculateProvider.GetMortgageResult(_mapper.Map<MortgageInput>(mortgageCalculateRequest));

            if (result.MonthlyCostAmount.Equals(0))
            {
                return new BadRequestObjectResult("Invalid User Input, interest rate not available for provided maturityPeriod ");
            }

            return new JsonResult(_mapper.Map<MortgageCalculateResponse>(result));
        }
    }
}
