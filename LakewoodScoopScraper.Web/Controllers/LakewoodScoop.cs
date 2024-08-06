using LakewoodScoopScraper.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LakewoodScoopScraper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LakewoodScoop : ControllerBase
    {
        [Route("scrape")]
        public List<Headline> ScrapeScoop()
        {
            var scraper = new Scraper();
            return scraper.Scrape();
        }
    }
}
