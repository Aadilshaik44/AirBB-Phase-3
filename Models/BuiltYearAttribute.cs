using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AirBB.Models;

public class BuiltYearAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly int _maxAgeYears;

    public BuiltYearAttribute(int maxAgeYears)
    {
        _maxAgeYears = maxAgeYears;
    }

    // Server-side Validation
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is int year)
        {
            int currentYear = DateTime.Now.Year;
            if (year > currentYear)
            {
                return new ValidationResult("Built Year cannot be in the future.");
            }
            if (year < currentYear - _maxAgeYears)
            {
                return new ValidationResult($"Built Year cannot be older than {_maxAgeYears} years.");
            }
            return ValidationResult.Success;
        }
        
        // If value is null or not an int, let [Required] handle it
        return ValidationResult.Success;
    }

    // Client-side Validation (adds data-val attributes)
    public void AddValidation(ClientModelValidationContext context)
    {
        if (!context.Attributes.ContainsKey("data-val"))
            context.Attributes.Add("data-val", "true");

        context.Attributes.Add("data-val-builtyear", ErrorMessage ?? "Invalid built year.");
        context.Attributes.Add("data-val-builtyear-maxyears", _maxAgeYears.ToString());
    }
}