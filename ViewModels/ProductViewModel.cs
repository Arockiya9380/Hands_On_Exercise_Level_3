namespace Level_3___Advanced_Patterns.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPercentage { get; set; }
        
        // Calculated property for discounted price
        public decimal FinalPrice
        {
            get
            {
                if (DiscountPercentage.HasValue && DiscountPercentage.Value > 0)
                {
                    return Price - (Price * DiscountPercentage.Value / 100);
                }
                return Price;
            }
        }
        
        public int Rating { get; set; }
        public string Slug { get; set; }
        public string CategoryName { get; set; }
        public bool IsFeatured { get; set; }
    }
}