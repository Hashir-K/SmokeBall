using Microsoft.AspNetCore.Mvc;
using SmokeBall.BusinessLogic.Services.Interfaces;
using SmokeBall.Common.DTOs;

namespace SmokeBall.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IKeywordSearchService _keywordSearchService;

        public SearchController(ILogger<SearchController> logger, IKeywordSearchService keywordSearchService)
        {
            _logger = logger;
            _keywordSearchService = keywordSearchService;
        }

        [HttpPost]
        public async Task<ActionResult<SearchResponse>> SearchKeywords(SearchRequest searchRequest)
        {
            try
            {
                var count = await _keywordSearchService.GetSearchCount(searchRequest.URI, searchRequest.Keywords,searchRequest.NumberOfResults);
                return Ok(new SearchResponse { Count = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(SearchController));

                return BadRequest(ex);
            }
        }
    }
}
