using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace LakewoodScoopScraper.Web.Services
{

    public class Headline
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }


    public class Scraper
    {
        public List<Headline> Scrape()
        {
            var query = "https://thelakewoodscoop.com";
            var html = GetNewsHtml(query);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);

            var resultDivs = document.QuerySelectorAll("div.td-category-pos-image");
            return resultDivs.Select(div => ParseItem(div)).Where(i => i != null).ToList();

        }

        private static string GetNewsHtml(string query)
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
                UseCookies = true
            };

            var client = new HttpClient(handler);
            return client.GetStringAsync(query).Result;
        }

        private static Headline ParseItem(IElement div)
        {
            var headline = new Headline();

            var titleElement = div.QuerySelector("h3.td-module-title");
            headline.Title = titleElement?.TextContent;

            var anchorTag = div.QuerySelector("h3.td-module-title a");
            headline.Url = anchorTag?.Attributes["href"].Value;

            var imageElement = div.QuerySelector("span.entry-thumb");
            headline.Image = imageElement?.Attributes["data-img-url"].Value;

            var shortDescriptionElement = div.QuerySelector("div.td-excerpt");
            headline.Description = shortDescriptionElement?.TextContent;

            return headline;
        }
    }
}
