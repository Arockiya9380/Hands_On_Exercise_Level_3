using System.Text.RegularExpressions;

namespace Level_3___Advanced_Patterns.Helpers
{
    public static class HtmlSanitizer
    {
        private static readonly string[] AllowedTags = 
        { 
            "p", "br", "strong", "em", "u", "b", "i",
            "h1", "h2", "h3", "h4", "h5", "h6",
            "ul", "ol", "li", "a", "span", "div"
        };
        
        public static string Sanitize(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;
            
            // Remove script tags and their content
            html = Regex.Replace(html, @"<script[^>]*>.*?</script>", "", 
                RegexOptions.IgnoreCase | RegexOptions.Singleline);
            
            // Remove dangerous event handlers
            html = Regex.Replace(html, @"\son\w+\s*=\s*[""'][^""']*[""']", "", 
                RegexOptions.IgnoreCase);
            
            // Remove javascript: protocol
            html = Regex.Replace(html, @"javascript:", "", 
                RegexOptions.IgnoreCase);
            
            // Remove style attributes (optional - can allow safe CSS)
            html = Regex.Replace(html, @"\sstyle\s*=\s*[""'][^""']*[""']", "", 
                RegexOptions.IgnoreCase);
            
            return html;
        }
    }
}