using SmokeBall.BusinessLogic.Services.Implementations;

namespace SmokeBall.UnitTests
{
    public class Tests
    {
        private const string SEARCH_SERVICE_URL = "https://www.google.com.au/search?";

        private GoogleSearchService _searchService;

        [SetUp]
        public void Setup()
        {
            _searchService = new GoogleSearchService(SEARCH_SERVICE_URL);
        }

        [Test]
        public async Task No_SearchUrl_Parameter_Should_Fail()
        {
            await _searchService.GetSearchCount("", "test", 100);
        }

        [Test]
        public async Task No_Keyword_Parameter_Should_Fail()
        {
            await _searchService.GetSearchCount("www.smokeball.com.au", "", 100);
        }

        [Test]
        public async Task Wrong_ResultCount_Parameter_Should_Fail()
        {
            await _searchService.GetSearchCount("www.smokeball.com.au", "software", -1);
        }

        [Test]
        public async Task No_Keyword_Wrong_ResultCount_Parameters_Should_Fail()
        {
            await _searchService.GetSearchCount("www.smokeball.com.au", "", -1);
        }

        [Test]
        public async Task Correct_Parameters_Should_Pass()
        {
            var count = await _searchService.GetSearchCount("www.smokeball.com.au", "conveyance software", 100);

            Assert.Greater(count, 0);
        }
    }
}