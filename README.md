**Requirements : Wep Api Application**

Backend application using REST and it should contain the following endpoints;

    GET /api/interest-rates (get a list of current interest rates)
    POST /api/mortgage-check (post the parameters to calculate for a mortgage check)

The list of current mortgage rates should be created in memory on application startup. The mortgage rate object contains the fields; maturityPeriod (integer), interestRate (Percentage) and lastUpdate (Timestamp) The posted data for the mortgage check contains at least the fields; income (Amount), maturityPeriod (integer), loanValue (Amount), homeValue (Amount). The mortgage check return if the mortgage is feasible (boolean) and the montly costs (Amount) of the mortgage.

Business rules that apply are

    a mortgage should not exceed 4 times the income
    a mortgage should not exceed the home value
    

**Framework Used**
* DotnetCore 3.1 - Web Api
* Entity Framework in Memory
* Xunit Testing
* MOQ mocking framework
* NBuilder
* AutoMapper

**Assumption made**
* Test data should reside in memory and to get loaded during Application Startup. Used "UseInMemory" concepts as a data provider in the Get Method.
* Maturity Period - In Years
* IncomeAmount - Annunal Income

**Get Method (~/api/interest-rates) Response** 

`[
  {
    "maturityPeriod": 1,
    "interestRate": 2.19,
    "lastUpdatedTime": "2020-06-01T01:18:09.175868+02:00"
  },
  {
    "maturityPeriod": 2,
    "interestRate": 2.09,
    "lastUpdatedTime": "2020-05-31T01:18:09.1792017+02:00"
  },
  {
    "maturityPeriod": 3,
    "interestRate": 2.14,
    "lastUpdatedTime": "2020-05-30T01:18:09.1792071+02:00"
  },
  {
    "maturityPeriod": 4,
    "interestRate": 2.49,
    "lastUpdatedTime": "2020-05-29T01:18:09.1792077+02:00"
  },
  {
    "maturityPeriod": 5,
    "interestRate": 2.54,
    "lastUpdatedTime": "2020-05-28T01:18:09.1792081+02:00"
  }
]
`

**PostMethod Request - ~/api/mortgage-check**

Request Structure Format : 

* Added Validation to validate the request
* IncomeAmount - Annual
* MaturityPeroid - In Years
`{
  "incomeAmount": 45000,
  "maturityPeriod": 5,
  "loanValueAmount": 300000,
  "homeValueAmount": 250000
}
`

Sample Respone:
`
{
  "monthlyCostAmount": 1329.50,
  "mortgageEligibility": false
}
`
