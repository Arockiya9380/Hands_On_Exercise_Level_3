using Microsoft.AspNetCore.Mvc;
using Level_3___Advanced_Patterns.Data;
using Level_3___Advanced_Patterns.ViewModels;

namespace Level_3___Advanced_Patterns.ViewComponents
{
    public class FeaturedProductsViewComponent : ViewComponent
    {
        // No dependency injection - access DataStore directly
        
        public async Task<IViewComponentResult> InvokeAsync(int count = 5)
        {
            // Get featured products directly from DataStore
            var products = DataStore.Products
                .Where(p => p.IsFeatured)
                .OrderByDescending(p => p.Rating)
                .Take(count)
                .Select(p => MapToViewModel(p))
                .ToList();
            
            return await Task.FromResult(View(products));
        }
        
        // Helper method to map Product to ProductViewModel
        private ProductViewModel MapToViewModel(Models.Product product)
        {
            var category = DataStore.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                Description = product.Description,
                Cost = product.Cost,
                Price = product.Price,
                DiscountPercentage = product.DiscountPercentage,
                Rating = product.Rating,
                Slug = product.Slug,
                CategoryName = category?.Name ?? "Unknown",
                IsFeatured = product.IsFeatured
            };
        }
    }
}
