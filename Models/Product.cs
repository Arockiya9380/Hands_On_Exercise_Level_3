using System.ComponentModel.DataAnnotations;

namespace Level_3___Advanced_Patterns.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "SKU is required")]
        [StringLength(50, ErrorMessage = "SKU cannot exceed 50 characters")]
        public string SKU { get; set; }
        
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Cost is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Cost must be between 0.01 and 999999.99")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }
        
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999999.99")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
        public decimal? DiscountPercentage { get; set; }
        
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
        
        public string Slug { get; set; }
        
        public bool IsFeatured { get; set; }
        
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        
        // Navigation property
        public virtual Category Category { get; set; }
    }
}