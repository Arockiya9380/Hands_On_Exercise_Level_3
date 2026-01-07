using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Level_3___Advanced_Patterns.Validation;

namespace Level_3___Advanced_Patterns.ViewModels
{
    public class ProductCreateViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "SKU is required")]
        [StringLength(50, ErrorMessage = "SKU cannot exceed 50 characters")]
        [Remote(action: "VerifySKU", controller: "Products", ErrorMessage = "This SKU already exists")]
        [Display(Name = "SKU (Stock Keeping Unit)")]
        public string SKU { get; set; }
        
        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Cost price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Cost must be between 0.01 and 999999.99")]
        [DataType(DataType.Currency)]
        [Display(Name = "Cost Price")]
        public decimal Cost { get; set; }
        
        [Required(ErrorMessage = "Selling price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999999.99")]
        [DataType(DataType.Currency)]
        [ValidPrice(ErrorMessage = "Selling price must be greater than cost price")]
        [Display(Name = "Selling Price")]
        public decimal Price { get; set; }
        
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
        [Display(Name = "Discount Percentage")]
        public decimal? DiscountPercentage { get; set; }
        
        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        [Display(Name = "Product Rating")]
        public int Rating { get; set; }
        
        [Required(ErrorMessage = "Please select a category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        [Display(Name = "Featured Product")]
        public bool IsFeatured { get; set; }
        
        // For dropdown list
        public IEnumerable<SelectListItem> Categories { get; set; }
        
        // Complex business rule validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check if discount makes the final price less than cost
            if (DiscountPercentage.HasValue && DiscountPercentage.Value > 0)
            {
                var discountedPrice = Price - (Price * DiscountPercentage.Value / 100);
                
                if (discountedPrice < Cost)
                {
                    yield return new ValidationResult(
                        $"After applying {DiscountPercentage}% discount, the final price (${discountedPrice:N2}) cannot be less than the cost price (${Cost:N2}). You would be selling at a loss!",
                        new[] { nameof(DiscountPercentage) }
                    );
                }
            }
        }
    }
}