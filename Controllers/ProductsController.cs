using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Level_3___Advanced_Patterns.Data;
using Level_3___Advanced_Patterns.Models;
using Level_3___Advanced_Patterns.ViewModels;
using Level_3___Advanced_Patterns.Helpers;

namespace Level_3___Advanced_Patterns.Controllers
{
    [Route("products")]
    public class ProductsController : Controller
    {
        // NO dependency injection - work directly with DataStore
        
        // GET: /products
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var products = DataStore.Products
                .Select(p => MapToViewModel(p))
                .ToList();
            
            return await Task.FromResult(View(products));
        }
        
        // GET: /products/category/electronics
        [HttpGet]
        [Route("category/{categorySlug}")]
        public async Task<IActionResult> Category(string categorySlug)
        {
            var products = DataStore.Products
                .Where(p => p.Category != null && p.Category.Slug == categorySlug)
                .Select(p => MapToViewModel(p))
                .ToList();
            
            ViewBag.CategorySlug = categorySlug;
            return await Task.FromResult(View(products));
        }
        
        // GET: /products/electronics/iphone-14-pro (SEO-friendly URL)
        [HttpGet]
        [Route("{category}/{slug}")]
        public async Task<IActionResult> Details(string category, string slug)
        {
            var product = DataStore.Products.FirstOrDefault(p => 
                p.Slug == slug && 
                p.Category != null && 
                p.Category.Slug == category
            );
            
            if (product == null)
            {
                return NotFound();
            }
            
            var viewModel = MapToViewModel(product);
            return await Task.FromResult(View(viewModel));
        }
        
        // GET: /products/create
        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create()
        {
            var model = new ProductCreateViewModel
            {
                Categories = GetCategoriesSelectList()
            };
            return await Task.FromResult(View(model));
        }
        
        // POST: /products/create
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = DataStore.Categories.FirstOrDefault(c => c.Id == model.CategoryId);
                    
                    if (category == null)
                    {
                        ModelState.AddModelError("CategoryId", "Invalid category selected");
                    }
                    else
                    {
                        var product = new Product
                        {
                            Id = DataStore.GetNextProductId(),
                            Name = model.Name,
                            SKU = model.SKU,
                            Description = HtmlSanitizer.Sanitize(model.Description),
                            Cost = model.Cost,
                            Price = model.Price,
                            DiscountPercentage = model.DiscountPercentage,
                            Rating = model.Rating,
                            Slug = GenerateSlug(model.Name),
                            IsFeatured = model.IsFeatured,
                            CategoryId = model.CategoryId,
                            Category = category
                        };
                        
                        DataStore.Products.Add(product);
                        
                        TempData["SuccessMessage"] = "Product created successfully!";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Unable to create product. Please try again.");
                }
            }
            
            model.Categories = GetCategoriesSelectList();
            return await Task.FromResult(View(model));
        }
        
        // GET: /products/edit/5 (with route constraint)
        [HttpGet]
        [Route("edit/{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }
            
            var model = new ProductCreateViewModel
            {
                Name = product.Name,
                SKU = product.SKU,
                Description = product.Description,
                Cost = product.Cost,
                Price = product.Price,
                DiscountPercentage = product.DiscountPercentage,
                Rating = product.Rating,
                CategoryId = product.CategoryId,
                IsFeatured = product.IsFeatured,
                Categories = GetCategoriesSelectList()
            };
            
            return await Task.FromResult(View(model));
        }
        
        // POST: /products/edit/5
        [HttpPost]
        [Route("edit/{id:int:min(1)}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
                    
                    if (product == null)
                    {
                        return NotFound();
                    }
                    
                    var category = DataStore.Categories.FirstOrDefault(c => c.Id == model.CategoryId);
                    
                    if (category == null)
                    {
                        ModelState.AddModelError("CategoryId", "Invalid category selected");
                    }
                    else
                    {
                        product.Name = model.Name;
                        product.SKU = model.SKU;
                        product.Description = HtmlSanitizer.Sanitize(model.Description);
                        product.Cost = model.Cost;
                        product.Price = model.Price;
                        product.DiscountPercentage = model.DiscountPercentage;
                        product.Rating = model.Rating;
                        product.Slug = GenerateSlug(model.Name);
                        product.IsFeatured = model.IsFeatured;
                        product.CategoryId = model.CategoryId;
                        product.Category = category;
                        
                        TempData["SuccessMessage"] = "Product updated successfully!";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Unable to update product. Please try again.");
                }
            }
            
            model.Categories = GetCategoriesSelectList();
            return await Task.FromResult(View(model));
        }
        
        // POST: /products/delete/5
        [HttpPost]
        [Route("delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
                
                if (product == null)
                {
                    TempData["ErrorMessage"] = "Product not found.";
                }
                else
                {
                    DataStore.Products.Remove(product);
                    TempData["SuccessMessage"] = "Product deleted successfully!";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Unable to delete product.";
            }
            
            return await Task.FromResult(RedirectToAction(nameof(Index)));
        }
        
        // GET: /products/verify-sku (Remote Validation Endpoint)
        [HttpGet]
        [Route("verify-sku")]
        public async Task<IActionResult> VerifySKU(string sku)
        {
            var exists = DataStore.Products.Any(p => 
                p.SKU.Equals(sku, StringComparison.OrdinalIgnoreCase)
            );
            
            return await Task.FromResult(Json(!exists)); // Return true if available, false if taken
        }
        
        // ========================================================================
        // HELPER METHODS
        // ========================================================================
        
        // Map Product entity to ProductViewModel
        private ProductViewModel MapToViewModel(Product product)
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
        
        // Generate SEO-friendly slug from product name
        private string GenerateSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;
            
            return name.ToLower()
                .Replace(" ", "-")
                .Replace("&", "and")
                .Replace(",", "")
                .Replace(".", "")
                .Replace("'", "")
                .Replace("\"", "");
        }
        
        // Get categories for dropdown list
        private IEnumerable<SelectListItem> GetCategoriesSelectList()
        {
            return DataStore.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
        }
    }
} 

// //                 Cost = productEntity.Cost,
// //                 Price = productEntity.Price,
// //                 DiscountPercentage = productEntity.DiscountPercentage,
// //                 Rating = productEntity.Rating,
// //                 CategoryId = productEntity.CategoryId,
// //                 IsFeatured = productEntity.IsFeatured,
// //                 Categories = GetCategoriesSelectList()
// //             };
            
// //             return View(model);
// //         }
        
//         // POST: /products/edit/5
//         [HttpPost]
//         [Route("edit/{id:int:min(1)}")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, ProductCreateViewModel model)
//         {
//             if (ModelState.IsValid)
//             {
//                 var success = await _productService.UpdateProductAsync(id, model);
                
//                 if (success)
//                 {
//                     TempData["SuccessMessage"] = "Product updated successfully!";
//                     return RedirectToAction(nameof(Index));
//                 }
                
//                 ModelState.AddModelError("", "Unable to update product. Please try again.");
//             }
            
//             model.Categories = GetCategoriesSelectList();
//             return View(model);
//         }
        
//         // POST: /products/delete/5
//         [HttpPost]
//         [Route("delete/{id:int}")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Delete(int id)
//         {
//             var success = await _productService.DeleteProductAsync(id);
            
//             if (success)
//             {
//                 TempData["SuccessMessage"] = "Product deleted successfully!";
//             }
//             else
//             {
//                 TempData["ErrorMessage"] = "Unable to delete product.";
//             }
            
//             return RedirectToAction(nameof(Index));
//         }
        
//         // GET: /products/verify-sku (Remote Validation Endpoint)
//         [HttpGet]
//         [Route("verify-sku")]
//         public async Task<IActionResult> VerifySKU(string sku)
//         {
//             var exists = await _productService.SKUExistsAsync(sku);
//             return Json(!exists); // Return true if available, false if taken
//         }
        
//         // Helper method to get categories for dropdown
//         private IEnumerable<SelectListItem> GetCategoriesSelectList()
//         {
//             return DataStore.Categories.Select(c => new SelectListItem
//             {
//                 Value = c.Id.ToString(),
//                 Text = c.Name
//             });
//         }
//     }
// }