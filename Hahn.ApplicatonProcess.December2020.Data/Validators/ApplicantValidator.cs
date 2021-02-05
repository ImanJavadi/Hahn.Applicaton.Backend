using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Api.Services;
using Hahn.ApplicatonProcess.December2020.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Api.Utilities.Validators
{
    public class ApplicantValidator : AbstractValidator<ModelDB>
    {
        public ApplicantValidator()
        {
            RuleFor(applicant => applicant.Name).NotEmpty().MinimumLength(5); ;
            RuleFor(applicant => applicant.FamilyName).NotEmpty().MinimumLength(5);
            RuleFor(applicant => applicant.Address).NotEmpty().MinimumLength(10);
            RuleFor(applicant => applicant.CountryOfOrigin).NotEmpty();
            RuleFor(applicant => applicant.EmailAddress).NotEmpty().EmailAddress();
            RuleFor(applicant => applicant.Age).NotEmpty().InclusiveBetween(20, 60);
            RuleFor(applicant => applicant.CountryOfOrigin).MustAsync(CheckIfCountryValid).WithMessage("Invalid CountryOfOrigin");
            RuleFor(applicant => applicant.Hired).Must(x => x == false || x == true);  
        }

      
        public async Task<bool> CheckIfCountryValid(string countryTxt, CancellationToken cancellationToken)
        {
            var countriesResponse = await HahnExternalServices.GetMatchingCountries(countryTxt, cancellationToken);
            return countriesResponse.IsSuccessStatusCode;
        }
    }

}
