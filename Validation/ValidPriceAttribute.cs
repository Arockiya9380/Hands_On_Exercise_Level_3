using System.ComponentModel.DataAnnotations;

namespace Level_3___Advanced_Patterns.Validation
{
    public class ValidPriceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the price value
            if (value == null)
            {
                return ValidationResult.Success; // Let Required handle null
            }
            
            var price = (decimal)value;
            
            // Get the Cost property from the model using reflection
            var costProperty = validationContext.ObjectType.GetProperty("Cost");
            
            if (costProperty == null)
            {
                return new ValidationResult("Cost property not found on the model");
            }
            
            // Get the cost value from the model instance
            var costValue = costProperty.GetValue(validationContext.ObjectInstance);
            
            if (costValue == null)
            {
                return ValidationResult.Success; // Can't validate if cost is null
            }
            
            var cost = (decimal)costValue;
            
            // Validate that price is greater than cost
            if (price <= cost)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"Selling price (${price:N2}) must be greater than cost price (${cost:N2})",
                    new[] { validationContext.MemberName }
                );
            }
            
            return ValidationResult.Success;
        }
    }
}
