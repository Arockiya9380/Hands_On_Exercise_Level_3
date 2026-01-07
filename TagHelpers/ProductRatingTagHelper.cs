using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace Level_3___Advanced_Patterns.TagHelpers
{
    [HtmlTargetElement("product-rating")]
    public class ProductRatingTagHelper : TagHelper
    {
        [HtmlAttributeName("stars")]
        public int Stars { get; set; }
        
        [HtmlAttributeName("max-stars")]
        public int MaxStars { get; set; } = 5;
        
        [HtmlAttributeName("css-class")]
        public string CssClass { get; set; } = "rating";
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", CssClass);
            
            var sb = new StringBuilder();
            
            // Ensure stars doesn't exceed max stars
            var filledStars = Math.Min(Stars, MaxStars);
            
            // Render filled stars
            for (int i = 0; i < filledStars; i++)
            {
                sb.Append("<span class='star filled'>★</span>");
            }
            
            // Render empty stars
            for (int i = filledStars; i < MaxStars; i++)
            {
                sb.Append("<span class='star empty'>☆</span>");
            }
            
            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}