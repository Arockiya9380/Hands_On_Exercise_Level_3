using System.ComponentModel.DataAnnotations;

namespace Level_3___Advanced_Patterns.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }
        
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; }
        
        public string Slug { get; set; }
        
        // Navigation property
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
