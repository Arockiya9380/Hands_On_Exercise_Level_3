using Level_3___Advanced_Patterns.Models;

namespace Level_3___Advanced_Patterns.Data
{
    public static class DataStore
    {
        private static int _nextProductId = 1;
        private static int _nextCategoryId = 1;
        
        public static List<Category> Categories { get; private set; }
        public static List<Product> Products { get; private set; }
        
        static DataStore()
        {
            InitializeData();
        }
        
        private static void InitializeData()
        {
            // Initialize Categories
            Categories = new List<Category>
            {
                new Category 
                { 
                    Id = _nextCategoryId++, 
                    Name = "Electronics", 
                    Slug = "electronics",
                    Description = "Electronic devices and gadgets"
                },
                new Category 
                { 
                    Id = _nextCategoryId++, 
                    Name = "Clothing", 
                    Slug = "clothing",
                    Description = "Fashion and apparel"
                },
                new Category 
                { 
                    Id = _nextCategoryId++, 
                    Name = "Books", 
                    Slug = "books",
                    Description = "Books and publications"
                },
                new Category 
                { 
                    Id = _nextCategoryId++, 
                    Name = "Home & Garden", 
                    Slug = "home-garden",
                    Description = "Home improvement and garden supplies"
                }
            };
            
            // Initialize Products
            Products = new List<Product>
            {
                // Electronics
                new Product
                {
                    Id = _nextProductId++,
                    Name = "iPhone 14 Pro",
                    SKU = "IPH14PRO",
                    Description = "<h3>Latest iPhone with advanced features</h3><p>The iPhone 14 Pro features a stunning <strong>Super Retina XDR display</strong>, powerful A16 Bionic chip, and an advanced camera system.</p><ul><li>6.1-inch display</li><li>48MP main camera</li><li>All-day battery life</li></ul>",
                    Cost = 800.00m,
                    Price = 999.00m,
                    DiscountPercentage = 10m,
                    Rating = 5,
                    Slug = "iphone-14-pro",
                    IsFeatured = true,
                    CategoryId = 1
                },
                new Product
                {
                    Id = _nextProductId++,
                    Name = "Samsung Galaxy Watch 5",
                    SKU = "SGW5",
                    Description = "<h3>Premium smartwatch with health tracking</h3><p>Track your fitness goals with <strong>advanced health sensors</strong> and GPS.</p>",
                    Cost = 150.00m,
                    Price = 279.99m,
                    DiscountPercentage = 15m,
                    Rating = 4,
                    Slug = "samsung-galaxy-watch-5",
                    IsFeatured = true,
                    CategoryId = 1
                },
                new Product
                {
                    Id = _nextProductId++,
                    Name = "Sony WH-1000XM5 Headphones",
                    SKU = "SONYWH1000XM5",
                    Description = "<h3>Industry-leading noise cancellation</h3><p>Experience premium sound quality with the best noise cancellation technology.</p>",
                    Cost = 250.00m,
                    Price = 399.99m,
                    Rating = 5,
                    Slug = "sony-wh-1000xm5-headphones",
                    IsFeatured = true,
                    CategoryId = 1
                },
                
                // Clothing
                new Product
                {
                    Id = _nextProductId++,
                    Name = "Levi's 501 Original Jeans",
                    SKU = "LEV501",
                    Description = "<h3>Classic straight fit jeans</h3><p>The original blue jean since 1873. A classic straight fit with a timeless style.</p>",
                    Cost = 40.00m,
                    Price = 69.99m,
                    DiscountPercentage = 20m,
                    Rating = 4,
                    Slug = "levis-501-original-jeans",
                    IsFeatured = false,
                    CategoryId = 2
                },
                new Product
                {
                    Id = _nextProductId++,
                    Name = "Nike Air Max 90",
                    SKU = "NIKEAM90",
                    Description = "<h3>Iconic running shoes</h3><p>A classic design with visible Air cushioning and comfortable fit.</p>",
                    Cost = 80.00m,
                    Price = 129.99m,
                    Rating = 5,
                    Slug = "nike-air-max-90",
                    IsFeatured = true,
                    CategoryId = 2
                },
                
                // Books
                new Product
                {
                    Id = _nextProductId++,
                    Name = "Atomic Habits by James Clear",
                    SKU = "BOOK001",
                    Description = "<h3>Transform your life with tiny changes</h3><p>Learn how small habits can lead to remarkable results in this <strong>bestselling book</strong>.</p>",
                    Cost = 12.00m,
                    Price = 24.99m,
                    Rating = 5,
                    Slug = "atomic-habits",
                    IsFeatured = true,
                    CategoryId = 3
                },
                new Product
                {
                    Id = _nextProductId++,
                    Name = "The Pragmatic Programmer",
                    SKU = "BOOK002",
                    Description = "<h3>Your Journey To Mastery</h3><p>Essential reading for software developers looking to improve their craft.</p>",
                    Cost = 25.00m,
                    Price = 49.99m,
                    DiscountPercentage = 10m,
                    Rating = 5,
                    Slug = "the-pragmatic-programmer",
                    IsFeatured = false,
                    CategoryId = 3
                },
                
                // Home & Garden
                new Product
                {
                    Id = _nextProductId++,
                    Name = "Dyson V15 Detect Vacuum",
                    SKU = "DYSONV15",
                    Description = "<h3>Powerful cordless vacuum</h3><p>Laser detection technology reveals invisible dust for the most thorough clean.</p>",
                    Cost = 450.00m,
                    Price = 649.99m,
                    Rating = 4,
                    Slug = "dyson-v15-detect-vacuum",
                    IsFeatured = false,
                    CategoryId = 4
                }
            };
        }
        
        public static int GetNextProductId()
        {
            return _nextProductId++;
        }
        
        public static void ResetData()
        {
            _nextProductId = 1;
            _nextCategoryId = 1;
            InitializeData();
        }
    }
}